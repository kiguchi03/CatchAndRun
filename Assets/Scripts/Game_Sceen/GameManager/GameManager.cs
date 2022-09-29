using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

/// <summary>
/// フラグ、タイムライン、シーン遷移、タイムスケールを制御
/// </summary>
public class GameManager : MonoBehaviour
{
    RoseSpawn_GM roseSpawn_;

    [SerializeField]
    PlayerControll playerControll;

    [SerializeField]
    SpawnerControll spawnerControll;

    [SerializeField]
    List<VaseController> vaseControllers = new List<VaseController>();

    [SerializeField]
    TvScreen tvScreen;


    [Header("敵がプレイヤーを襲うタイムラインオブジェクト")]
    [SerializeField]
    PlayableDirector EnemyTimeLine;

    [SerializeField]
    GameObject Player;


    [Header("ブラックアウトのパネル")]
    [SerializeField]
    GameObject BlackPanel;

    [Header("敵が強化される消滅回数の間隔")]
    [SerializeField]
    int powerUpValue = 5;

    [Header("RedRose以外のRoseフラグ")]
    [SerializeField]
    List<FlagData> roseFlags = new List<FlagData>();

    [Header("RedRoseフラグ")]
    [SerializeField]
    FlagData redRoseFlag;

    [Header("バラ")]
    [SerializeField]
    List<Item> items = new List<Item>();



    // Start is called before the first frame update
    void Start()
    {
        roseFlags.Add(redRoseFlag);
        
        Cursor.lockState = CursorLockMode.Locked;

        BlackPanel.SetActive(false);

        roseSpawn_ = GetComponent<RoseSpawn_GM>();


        FadeManager.instance.FadeIn(1.0f);

        FlagManager.instance.GetSetGameOver = false;

        FlagManager.instance.GetSetGameClear = false;
    }

    // Update is called once per frame
    void Update()
    { 
        EnemyPowerUp();

        GameClear();

        EnemyEvent();
    }

    /// <summary>
    /// 敵の消滅が特定の回数に達したら敵を強化するメソッド
    /// </summary>
    private void EnemyPowerUp()
    {
        //敵の消滅回数がpowerUpValueに達し対応するroseFlagが立っていなかったら、敵をスポーンを止め、
        //対応するroseFlagを立て、テレビをノイズにする
        for (int i = 0; i < items.Count; i++)
        {
            if (spawnerControll.enemyFadeOutValue == powerUpValue * (i + 1) && !items[i].GetFlagData.GetSetIsBool)
            {
                FlagManager.instance.GetRoseFlags[i].GetSetIsBool = true;

                tvScreen.GetSetIsNoisy = true;

                tvScreen.GetSetMessage = items[i].GetRoseMessage;

                spawnerControll.GetSetNoSpawn = true;
            }
        }
    }

    /// <summary>
    /// 敵のタイムラインを制御するメソッド
    /// </summary>
    private void EnemyEvent()
    {
        if (FlagManager.instance.GetSetGameOver)
        {
            EnemyTimeLine.Play();

            InputManager.instance.GetSetNoInput = true;

            playerControll.GetSetIsMove = false;

            Player.GetComponent<AudioSource>().enabled = false;

            StartCoroutine(BlackOut((float)EnemyTimeLine.duration));
        }
    }

    /// <summary>
    /// 任意のシーンを任意の秒数の遅延後にロードするメソッド
    /// </summary>
    /// <param name="sec">遅延時間</param>
    /// <param name="sceneValue">シーン番号</param>
    /// <returns></returns>
    private IEnumerator SceneLoad(float sec, int sceneValue)
    {
        yield return new WaitForSeconds(sec);

        SceneManager.LoadScene(sceneValue);
    }

    /// <summary>
    /// 任意の秒数の遅延後にブラックパネルを表示するメソッド
    /// 敵に襲われた時に呼び出し
    /// </summary>
    /// <param name="sec">遅延時間</param>
    /// <returns></returns>
    private IEnumerator BlackOut(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);

        BlackPanel.SetActive(true);

        StartCoroutine(SceneLoad(2.0f, 1));
    }

    /// <summary>
    /// ゲームクリアした際に呼び出されるメソッド
    /// </summary>
    private void GameClear()
    {
        if (FlagManager.instance.GetSetGameClear)
        {
            spawnerControll.GetSetNoSpawn = true;

            FadeManager.instance.FadeOut(0.3f);

            StartCoroutine(SceneLoad(5.0f, 0));
        }
    }
}