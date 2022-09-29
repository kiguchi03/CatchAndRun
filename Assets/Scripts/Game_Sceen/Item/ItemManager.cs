using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの取得を制御
/// </summary>
public class ItemManager : MonoBehaviour
{
    //アイテムのID
    [SerializeField]
    private int itemID;

    //アイテムデータベース
    [SerializeField]
    private ItemDataBase ItemDataBase;

    [SerializeField]
    AudioClip itemGet;

    private void Start()
    {

    }

    public int GetItemID()
    {
        if(itemGet != null)
        {
            SoundManager.instance.PlaySE(itemGet);
        }

        Destroy(this.gameObject);
        return itemID;
    }
}
