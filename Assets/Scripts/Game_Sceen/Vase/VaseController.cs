using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 花瓶によるバラの受け取り、リセットを制御
/// </summary>
public class VaseController : GetInput
{
    [SerializeField]
    RoseSpawn_GM roseSpawn_GM;

    [SerializeField]
    PlayerItemManager playerItemManager;

    //アイテムデータベース
    [SerializeField]
    ItemDataBase itemDataBase;


    [Tooltip("子オブジェクトのバラ")]
    [SerializeField]
    GameObject RoseObj;

    [Header("対応する色のバラ")]
    [SerializeField]
    Item itemRose;

    [Header("バラを置いた時の音")]
    [SerializeField]
    AudioClip put;


    [Header("花瓶に設置されたバラをリセットする時間")]
    [SerializeField]
    float RoseResetTime = 30.0f;

    //バラがスポーンしているか
    bool isSpawnedRose;

    //バラがセットされているか
    bool isSetRose;

    //時間経過
    float seconds;


    public bool GetSetIsSetRose
    {
        get { return isSetRose; }
        set { isSetRose = value; }
    }

    public Item GetItemRose
    {
        get { return itemRose; }
    }


    private void Start()
    {
        itemRose.GetFlagData.GetSetIsBool = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //フラグが立っていてまだバラが出現していなければSpawnRoseを呼ぶ
        if (itemRose.GetFlagData.GetSetIsBool && !isSpawnedRose)
        {
            roseSpawn_GM.SpawnRose(itemRose.GetGameObj);

            isSpawnedRose = true;
        }

        RoseLimit();
    }

    /// <summary>
    /// 一定時間経つと花瓶にセットされたバラを消滅させるメソッド
    /// </summary>
    private void RoseLimit()
    {
        //バラが花瓶にセットされていたらカウントを始める
        if (isSetRose) seconds += Time.fixedDeltaTime;

        //カウントがバラのリセット時間を超過したら
        if (seconds > RoseResetTime)
        {
            //isSetRoseをfalse、targetオブジェクトを非アクティブにする
            ResetRose();
            //カウントをリセット
            seconds = 0.0f;

            isSpawnedRose = false;
        }
    }

    /// <summary>
    /// プレイヤーからバラを受け取るメソッド
    /// </summary>
    protected override void GetEKeyEvent()
    {
        //既にバラが置かれていたらreturn;
        if (isSetRose) return;

        //プレイヤーが対応した色のバラを削除
        if (playerItemManager.RemoveItemFromPlayer(itemRose))
        {
            SoundManager.instance.PlaySE(put);
            SetRose();
        }
    }

    /// <summary>
    /// バラをセットするメソッド
    /// </summary>
    public void SetRose()
    {
        RoseObj.SetActive(true);
        isSetRose = true;
    }

    /// <summary>
    /// セットされたバラを消滅させるメソッド
    /// </summary>
    public void ResetRose()
    {
        RoseObj.SetActive(false);
        isSetRose = false;
    }
}
