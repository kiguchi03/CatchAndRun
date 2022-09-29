using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のタイムラインのオーディオを制御
/// </summary>
public class EventEnemyControll : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    private float baseVolume;


    // Start is called before the first frame update
    void Start()
    {
        baseVolume = audioSource.volume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        audioSource.volume = baseVolume * MenuManager.instance.GetVolume;
    }
}
