using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムデータベース
/// </summary>
[CreateAssetMenu(fileName = "ItemDataBase",menuName = "CreateDateBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField]
    List<Item> itemList = new List<Item>();

    public List<Item> GetItemList
    {
        get { return itemList; }
    }
}
