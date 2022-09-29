using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バラの情報
/// </summary>
[SerializeField]
[CreateAssetMenu(fileName = "Item",menuName ="CreateItem")]
public class Item : ScriptableObject
{
    public enum Color
    {
        Black,
        Blue,
        Purple,
        Red,
    }

    [SerializeField]
    Color roseColor;

    [SerializeField]
    GameObject gameObj;

    [SerializeField]
    FlagData flagData;

    [SerializeField]
    int itemID;

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    string roseMessage;


    public Color GetRoseColor
    {
        get { return roseColor; }
    }

    public GameObject GetGameObj
    {
        get { return gameObj; }
    }

    public FlagData GetFlagData
    {
        get { return flagData; }
    }

    public int GetItemID
    {
        get { return itemID; }
    }

    public Sprite GetSprite
    {
        get { return sprite; }
    }

    public string GetRoseMessage
    {
        get { return roseMessage; }
    }
}
