using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 入力を制御
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager instance;


    private bool noInput;

    public bool GetSetNoInput
    {
        get { return noInput; }
        set { noInput = value; }
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
