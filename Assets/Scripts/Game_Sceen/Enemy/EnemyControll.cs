using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の能力、消滅、オーディオ、赤いバラの受け取りの制御
/// </summary>
public class EnemyControll : MonoBehaviour
{
    EnemyControll enemyControll;

    public PlayerControll playerControll;

    public SpawnerControll spawnerControll;

    public List<VaseController> vaseControllers = new List<VaseController>();

    EnemyNav enemyNav;

    NavMeshAgent navMeshAgent;

    Animator anim;

    //敵が消すライト
    public List<Light> lights = new List<Light>();

    [Tooltip("スポーンする時のボイス")]
    [SerializeField]
    AudioClip enemySpawnVoice;

    [Tooltip("消滅する時のボイス")]
    [SerializeField]
    AudioClip enemyFadeOutVoice;

    [Tooltip("クリア条件を満たした時のボイス")]
    [SerializeField]
    AudioClip enemyClearVoice;

    [Tooltip("消滅する際のパーティクル")]
    [SerializeField]
    GameObject fadeOutVFX;


    //カメラのレイを受付けないか
    bool NoRecieveRender;

    //敵のTimeLineが発生中か
    bool isAct;

    //時間経過
    float seconds;

    [Header("敵が消滅する制限時間")]
    [SerializeField]
    float limitSeconds = 15.0f;

    [Header("強化時の一度で消すライトの数")]
    [SerializeField]
    int lightControllValue = 2;

    [Header("強化時のスピードアップの倍率")]
    [SerializeField]
    [Range(1.0f, 2.0f)] float Speed_Increase;

    [Header("強化時のスピードダウンの倍率")]
    [SerializeField]
    [Range(0.1f, 0.9f)] float Speed_DecreaseRate;

    [Header("赤いバラ")]
    [SerializeField]
    Item itemRedRose;

    [Tooltip("敵の手の赤バラ")]
    [SerializeField]
    GameObject objRedRose;


    // Start is called before the first frame update
    void Start()
    { 
        enemyNav = GetComponent<EnemyNav>();

        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        objRedRose.SetActive(false);


        SoundManager.instance.PlaySE(enemySpawnVoice);

        EnemyAbility();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seconds += Time.fixedDeltaTime;

        //制限時間に達すると消滅
        if (seconds >= limitSeconds && !isAct)
        {
            StartCoroutine("enemyFadeOut");
            isAct = true;
        }
    }

    /// <summary>
    /// 強化時の敵の能力を制御するメソッド
    /// </summary>
    private void EnemyAbility()
    {
        //花瓶にバラがセットされておらず、花瓶に対応した色のroseFlagが立っていれば、バラの色に対応した能力を発動する
        for (int i = 0; i < vaseControllers.Count; i++)
        {
            if (!vaseControllers[i].GetSetIsSetRose && vaseControllers[i].GetItemRose.GetFlagData.GetSetIsBool)
            {
                switch (vaseControllers[i].GetItemRose.GetRoseColor)
                {
                    case Item.Color.Black:

                        LightsControll();

                        break;

                    case Item.Color.Blue:

                        enemyNav.GetSetNoRendered = true;

                        navMeshAgent.speed = navMeshAgent.speed * Speed_DecreaseRate;

                        break;

                    case Item.Color.Purple:

                        navMeshAgent.speed = navMeshAgent.speed * Speed_Increase;

                        break;
                }
            }
        }
    }

    /// <summary>
    /// アクティブなライトを探し、任意の数のライトをランダムに非アクティブにするメソッド　強化時に呼び出される
    /// </summary>
    private void LightsControll()
    {
        //アクティブなライト格納するリスト shiningLightlist
        List<Light> shiningLightlist = new List<Light>();

        for (int i = 0; i < lights.Count; i++)
        {
            if (lights[i].enabled)
            {
                shiningLightlist.Add(lights[i]);
            }
        }

        //shiningLightListのランダムなライトを非アクティブ化
        for (int i = 0; i < lightControllValue; i++)
        {
            if (shiningLightlist.Count > 0)
            {
                int randomValue = Random.Range(0, shiningLightlist.Count);
                shiningLightlist[randomValue].enabled = false;
                shiningLightlist.RemoveAt(randomValue);
            }
        }
    }

    /// <summary>
    /// 敵を消滅させるメソッド
    /// </summary>
    public void DestroyThis()
    {
        spawnerControll.enemyFadeOutValue++;

        Destroy(this.gameObject);
    }

    /// <summary>
    /// 敵が消滅する際のアクションを制御するメソッド
    /// </summary>
    /// <returns></returns>
    public IEnumerator enemyFadeOut()
    {
        SoundManager.instance.PlaySE(enemyFadeOutVoice);

        enemyNav.StopChase();

        enemyNav.enabled = false;

        yield return new WaitForSeconds(3);

        spawnerControll.GetSetIsSpawned = false;

        EnemyEffect(this.gameObject.transform.position, fadeOutVFX);

        DestroyThis();
    }

    /// <summary>
    /// 任意の場所に任意のエフェクトを発生させるメソッド
    /// </summary>
    /// <param name="pos">場所</param>
    /// <param name="effect">エフェクト</param>
    private void EnemyEffect(Vector3 pos,GameObject effect)
    {
        Instantiate(effect, pos, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
    }

    /// <summary>
    /// プレイヤーのレッドローズを受け取るメソッド
    /// </summary>
    /// <param name="enemy">敵オブジェクト</param>
    public void SetItem(GameObject enemy)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            enemyNav = enemy.GetComponent<EnemyNav>();

            enemyControll = enemy.GetComponent<EnemyControll>();

            enemyNav.StopChase();

            anim = enemy.GetComponent<Animator>();

            enemyControll.objRedRose.SetActive(true);

            enemyControll.isAct = true;

            enemyControll.StopCorou();

            enemyControll.playerControll.GetSetIsMove = false;

            enemyNav.enabled = false;

            anim.SetBool("GetRose", true);
        }

    }


    /// <summary>
    /// クリア時用の消滅メソッド
    /// GetRose Animationで呼び出し
    /// </summary>
    public void Enemy_Clear()
    {
        EnemyEffect(this.gameObject.transform.position, fadeOutVFX);

        DestroyThis();

        FlagManager.instance.GetSetGameClear = true;
    }


    /// <summary>
    /// クリア時用のボイスメソッド
    /// GetRose Animationで呼び出し
    /// </summary>
    public void ClearVoice()
    {
        SoundManager.instance.PlaySE(enemyClearVoice);
    }

    /// <summary>
    /// コルーチンを停止するメソッド
    /// </summary>
    private void StopCorou()
    {
        StopCoroutine("enemyFadeOut");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FlagManager.instance.GetSetGameOver = true;
        }
    }
}
