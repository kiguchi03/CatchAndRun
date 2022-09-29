using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの動き、Rayを制御
/// </summary>
public class CameraControll : MonoBehaviour
{
    Camera _camera;

    [SerializeField]
    MessageControll messageControll;

    [SerializeField]
    PlayerItemManager playerItemManager;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    EnemyControll enemyControll;

    EnemyNav enemyNav;

    [SerializeField]
    TvScreen tvScreen;


    LayerMask layerMask = 2048;


    //X,Y軸のカメラ回転値
    float Xrotate;
    float Yrotate;

    float cameraAngleX;
    float cameraAngleY;

    //敵がRayに当たっているか
    bool EnemyRendered;

    [Header("Rayを出すか")]
    [SerializeField]
    bool isRay = true;

    [Header("動くか")]
    [SerializeField]
    bool isMove = true;

    [Header("縦の動きの制限")]
    [SerializeField] float YrotateLimit = 45.0f;

    [Header("赤いバラ")]
    [SerializeField]
    Item itemRedRose;

    //カメラ感度
    static float sensi = 1.0f;


    public bool GetSetIsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    public bool GetSetEnemyRendered
    {
        get { return EnemyRendered; }
        set { EnemyRendered = value; }
    }

    public float GetSetSensi
    {
        get { return sensi; }
        set { sensi = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        MoveCotroll();
    }

    // Update is called once per frame
    void Update()
    {
        RayControll();
    }
    
    /// <summary>
    /// カメラの動きを制御するメソッド
    /// </summary>
    private void MoveCotroll()
    {
        if (!isMove) return;

        Xrotate = Input.GetAxis("Mouse X") * MenuManager.instance.GetSensi;
        Yrotate = Input.GetAxis("Mouse Y") * MenuManager.instance.GetSensi;

        cameraAngleX += Mathf.Atan(Xrotate) * Mathf.Rad2Deg * 2.0f;
        cameraAngleY += Mathf.Atan(Yrotate) * Mathf.Rad2Deg * 2.0f;

        cameraAngleY = Mathf.Clamp(cameraAngleY, -YrotateLimit, YrotateLimit);

        gameObject.transform.localEulerAngles = new Vector3(cameraAngleY, cameraAngleX, 0);
    }

    /// <summary>
    /// オブジェクトを検知するメソッド
    /// </summary>
    private void RayControll()
    {
        if (!isRay) return;

        RaycastHit hit;

        //Enemy検知専用
        if (Physics.Raycast(transform.position, this.gameObject.transform.forward, out hit, 5, layerMask))
        {
            if (hit.collider.CompareTag("enemy"))
            {
                if (enemyNav == null) enemyNav = hit.collider.gameObject.GetComponent<EnemyNav>();

                enemyNav.GetSetIsRendered = true;
            }
            else if (!hit.collider.CompareTag("enemy") || hit.collider == null)
            {
                if (enemyNav != null)
                {
                    enemyNav.GetSetIsRendered = false;
                }
            }
        }


        //アイテムやイベントオブジェクト用
        if (Physics.Raycast(transform.position, this.gameObject.transform.forward, out hit, 1))
        {
            switch (hit.collider.gameObject.tag)
            {
                //アイテムの場合
                case "Item":

                    messageControll.EKey_SetTrue();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        playerItemManager.AddItemToPlayer(hit.collider.gameObject.GetComponent<ItemManager>()
                            .GetItemID());
                    }

                    break;

                //花瓶、テレビスクリーンの場合
                case "EventObj":

                    messageControll.EKey_SetTrue();

                    hit.collider.gameObject.GetComponent<GetInput>().GetEKey();

                    break;

                //敵の場合
                case "enemy":

                    //プレイヤーがレッドローズを持っているかどうか
                    for (int i = 0; i < playerItemManager.GetHaveItemList.Count; i++)
                    {
                        if (itemRedRose.GetItemID == playerItemManager.GetHaveItemList[i].GetItemID)
                        {
                            messageControll.EKey_SetTrue();

                            enemyControll.SetItem(hit.collider.gameObject);
                        }
                    }

                    break;

                default:

                    break;
            }
        }
        else
        {
            messageControll.EKey_SetFalse();
        }
    }
}
