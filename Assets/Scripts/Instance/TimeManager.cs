using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイムスケールを制御
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;


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


    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
}
