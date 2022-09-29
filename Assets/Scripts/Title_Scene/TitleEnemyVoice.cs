using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルでの敵のボイスを制御
/// </summary>
public class TitleEnemyVoice : MonoBehaviour
{
    [Header("敵のボイス")]
    [SerializeField]
    List<AudioClip> enemyVoices = new List<AudioClip>();


    [Header("ボイスの間隔")]
    [SerializeField]
    float enemyVoiceTime = 20.0f;

    //時間経過
    float sec;


    // Update is called once per frame
    void FixedUpdate()
    {
        sec += Time.deltaTime;

        //ランダムで敵のボイズを発生させる
        if(sec >= enemyVoiceTime)
        {
            int randomValue = Random.Range(0, enemyVoices.Count);
            SoundManager.instance.PlaySE(enemyVoices[randomValue]);
            sec = 0.0f;
        }
    }
}
