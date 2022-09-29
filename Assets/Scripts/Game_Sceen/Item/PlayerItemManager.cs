using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの所持品の追加、削除を制御
/// </summary>
public class PlayerItemManager : MonoBehaviour
{
    //アイテムデータベース
    [SerializeField]
    ItemDataBase itemDataBase;

    //アイテム確認パネル
    [SerializeField]
    ItemPanel itemPanel;

    //プレイヤー所持品
    [SerializeField]
    List<Item> haveItemList = new List<Item>();

    public List<Item> GetHaveItemList
    {
        get { return haveItemList; }
    }

    /// <summary>
    /// 任意のアイテムを取得しプレイヤーの所持品に追加するメソッド
    /// </summary>
    /// <param name="ID">任意のアイテムID</param>
    public void AddItemToPlayer(int ID)
    {
        for(int i = 0;i < itemDataBase.GetItemList.Count; i++)
        {
            //IDがデータベースにあるアイテムのIDと一致したらそのアイテムをHaveItemListに追加
            if(ID == itemDataBase.GetItemList[i].GetItemID)
            {
                haveItemList.Add(itemDataBase.GetItemList[i]);

                itemPanel.SetItemImage(itemDataBase.GetItemList[i].GetSprite);
            }
        }
    }

    /// <summary>
    /// 任意のアイテムをプレイヤーの所持品から削除するメソッド
    /// </summary>
    /// <param name="item">任意のアイテム</param>
    /// <returns></returns>
    public bool RemoveItemFromPlayer(Item item)
    {
        for (int i = 0; i < haveItemList.Count; i++)
        {
            //haveItemListから一致するアイテムを削除
            if (item.GetItemID == haveItemList[i].GetItemID)
            {
                haveItemList.RemoveAt(i);
                itemPanel.RemoveImage(item.GetSprite);
                return true;
            }
        }
        return false;
    }
}
