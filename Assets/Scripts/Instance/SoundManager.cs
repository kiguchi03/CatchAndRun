using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドを制御
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource bgmAudio;

    AudioSource seAudio;

    private float volumeBgm;

    //BGMループ用
    bool isLoop;
 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgmAudio = gameObject.AddComponent<AudioSource>();
        seAudio = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// BGMを再生するメソッド
    /// </summary>
    /// <param name="bgmClip"></param>
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmAudio.clip = bgmClip;

        if (!bgmAudio.isPlaying)
        {
            bgmAudio.Play();
        }

        isLoop = true;
    }

    /// <summary>
    /// SEを再生するメソッド
    /// </summary>
    /// <param name="seClip"></param>
    public void PlaySE(AudioClip seClip)
    {
        seAudio.PlayOneShot(seClip);
    }

    /// <summary>
    /// BGMを止めるメソッド
    /// </summary>
    public void StopBGM()
    {
        bgmAudio.Stop();

        isLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        seAudio.volume = MenuManager.instance.GetVolume;
        bgmAudio.volume = MenuManager.instance.GetVolume;

        //クリップ終了１秒前になるとクリップの最初から再生する
        if (isLoop && bgmAudio.time >= bgmAudio.clip.length - 1.0f)
        {
            bgmAudio.time = 0.1f;

            bgmAudio.Stop();

            bgmAudio.Play();
        }
    }
}
