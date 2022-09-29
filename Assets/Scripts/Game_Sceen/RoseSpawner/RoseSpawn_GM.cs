using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バラのスポーン制御
public class RoseSpawn_GM : MonoBehaviour
{
    [Header("スポーンさせるバラ")]
    [SerializeField]
    List<Item> RoseItems = new List<Item>();

    //アイテムデータベース
    [SerializeField]
    ItemDataBase itemDataBase;


    [Header("バラのスポーン位置を示す空のオブジェクト")]
    [SerializeField]
    List<GameObject> roseSpawnPos = new List<GameObject>();


    /// <summary>
    /// プレイヤーの範囲外のランダムな場所に任意のバラをスポーンするメソッド
    /// </summary>
    /// <param name="Rose"></param>
    public void SpawnRose(GameObject Rose)
    {
        //バラが生成可能なroseSpawnPosを格納するリスト roseSpawnList
        List<GameObject> roseSpawnList = new List<GameObject>();

        for (int i = 0; i < roseSpawnPos.Count; i++)
        {
            //プレイヤーが範囲外で、まだバラがセットされていないSpawnPositionをspawnListに追加
            if (!roseSpawnPos[i].GetComponent<RoseSpawnPosition>().GetInPlayer() &&
                roseSpawnPos[i].transform.childCount == 0)
            {
                roseSpawnList.Add(roseSpawnPos[i]);
            }
        }

        int randomValue = Random.Range(0, roseSpawnList.Count);

        Instantiate(Rose, roseSpawnList[randomValue].transform.position,
            roseSpawnList[randomValue].transform.rotation,roseSpawnList[randomValue].transform);

        roseSpawnList.Clear();
    }
}
