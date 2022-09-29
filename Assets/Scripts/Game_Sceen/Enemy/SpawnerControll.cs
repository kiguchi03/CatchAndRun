using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵スポーンの開始、停止を制御
/// </summary>
public class SpawnerControll : MonoBehaviour
{
    SpawnerControll spawnerControll;

    [SerializeField]
    PlayerControll playerControll;

    [SerializeField]
    List<VaseController> vaseControllers = new List<VaseController>();


    [Header("スポーンする敵")]
    [SerializeField]
    GameObject target;

    [Header("敵のスポーン位置を示す空のオブジェクト")]
    [SerializeField]
    List<GameObject> spawnPosition = new List<GameObject>();

    [Header("ライトオブジェクト")]
    [SerializeField]
    List<Light> lights = new List<Light>();


    [Header("敵スポーンの間隔")]
    [SerializeField]
    float SpawnDelayTime = 20.0f;

    float seconds = 1.0f;

    public int enemyFadeOutValue;

    bool noSpawn;

    //既に敵をスポーンしているか
    bool isSpawned;


    public bool GetSetNoSpawn
    {
        get { return noSpawn; }
        set { noSpawn = value; }
    }

    public bool GetSetIsSpawned
    {
        get { return isSpawned; }
        set { isSpawned = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnerControll = GetComponent<SpawnerControll>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (noSpawn) return;

        //secondsが0.0になれば敵をスポーンさせる
        if (!isSpawned)
        {
            seconds -= Time.fixedDeltaTime;

            if (seconds > 0.0f) return;

            GenerateEnemy();

            seconds = SpawnDelayTime;
        }
    }

    /// <summary>
    /// 敵をランダムなspawnPositionの一箇所からスポーンさせる
    /// </summary>
    void GenerateEnemy()
    {
        //敵がスポーン可能なspawnPositionを格納するリスト　spawnList
        List<GameObject> spawnList = new List<GameObject>();

        //プレイヤーが範囲外にいるSpawnPositionをspawnListに追加
        for (int i = 0; i < spawnPosition.Count; i++)
        {
            if (!spawnPosition[i].GetComponent<SpawnPosition>().GetInPlayer())
            {
                spawnList.Add(spawnPosition[i]);
            }
        }

        int randomValue = Random.Range(0, spawnList.Count);


        //敵をスポーンし、スポーンした敵に必要なコンポーネントをアタッチする

        GameObject enemyPrefab = Instantiate(target,spawnList[randomValue].transform.position,
            Quaternion.identity);

        EnemyControll EneCon_EnePre = enemyPrefab.GetComponent<EnemyControll>();

        EnemyNav enemyNav_EnePre = enemyPrefab.GetComponent<EnemyNav>();

        EneCon_EnePre.spawnerControll = spawnerControll;

        EneCon_EnePre.playerControll = playerControll;

        EneCon_EnePre.vaseControllers = vaseControllers;

        EneCon_EnePre.lights = lights;


        isSpawned = true;
    }
}
