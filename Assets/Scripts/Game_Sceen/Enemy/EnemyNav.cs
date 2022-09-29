using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の追跡を制御
/// </summary>
public class EnemyNav : MonoBehaviour
{
    EnemyControll enemyControll;

    NavMeshAgent navMesh;


    //追跡対象
    GameObject target;

    //写っているか
    public bool isRendered;

    //カメラのRayを受け付けるかどうか
    public bool noRendered;

    //動くか
    bool isMove;

    public bool GetSetIsRendered
    {
        get { return isRendered; }
        set { isRendered = value; }
    }

    public bool GetSetNoRendered
    {
        get { return noRendered; }
        set { noRendered = value; }
    }

    public bool GetSetIsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        enemyControll = GetComponent<EnemyControll>();

        target = GameObject.FindGameObjectWithTag("Player");

        isMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (navMesh.isOnNavMesh)
        {
            //プレイヤーのカメラのRayに敵が当たっていたら追跡停止
            if ((!isRendered || noRendered) && isMove)
            {
                StartChase();

                navMesh.destination = target.transform.position;
            }
            else
            {
                StopChase();
            }
        }
    }

    /// <summary>
    /// 追跡停止メソッド
    /// </summary>
    public void StopChase()
    {
        navMesh.isStopped = true;
    }

    /// <summary>
    /// 追跡開始メソッド
    /// </summary>
    public void StartChase()
    {
        navMesh.isStopped = false;
    }
}
