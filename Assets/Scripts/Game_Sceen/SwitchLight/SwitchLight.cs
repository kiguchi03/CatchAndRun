using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ライトを制御
/// </summary>
public class SwitchLight : GetInput
{
    [SerializeField]
    private Light _light;

    [Header("押した音")]
    [SerializeField]
    AudioClip push;

    [Header("遅延秒数")]
    [SerializeField]
    float delayTime = 1.0f;

    [SerializeField]
    bool notInput;


    /// <summary>
    /// Eキーを押すとライトのアクティブ・非アクティブを切り替えるメソッド
    /// </summary>
    protected override void GetEKeyEvent()
    {
        if (!notInput)
        {
            StartCoroutine(Delay(delayTime));

            _light.enabled = !_light.enabled!;

            SoundManager.instance.PlaySE(push);
        }
    }

    IEnumerator Delay(float sec)
    {
        notInput = true;

        yield return new WaitForSecondsRealtime(sec);

        notInput = false;
    }
}
