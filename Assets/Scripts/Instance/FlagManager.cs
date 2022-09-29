using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フラグを制御
/// </summary>
public class FlagManager : MonoBehaviour
{
    public static FlagManager instance;


    [SerializeField]
    List<FlagData> roseFlags = new List<FlagData>();

    [SerializeField]
    FlagData redRose;

    [SerializeField]
    FlagData tvScreen;

    [SerializeField]
    FlagData gameClear;

    [SerializeField]
    FlagData gameOver;


    public List<FlagData> GetRoseFlags
    {
        get { return roseFlags; }
    }

    public bool GetSetRedRose
    {
        get { return redRose.GetSetIsBool; }
        set { redRose.GetSetIsBool = value; }
    }

    public bool GetSetTvScreen
    {
        get { return tvScreen.GetSetIsBool; }
        set { tvScreen.GetSetIsBool = value; }
    }

    public bool GetSetGameClear
    {
        get { return gameClear.GetSetIsBool; }
        set { gameClear.GetSetIsBool = value; }
    }

    public bool GetSetGameOver
    {
        get { return gameOver.GetSetIsBool; }
        set { gameOver.GetSetIsBool = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
