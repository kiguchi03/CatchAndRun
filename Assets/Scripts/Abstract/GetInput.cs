using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eキー押すと特定のイベントを起こしたい時にオーバーライド
/// </summary>
public abstract class GetInput : MonoBehaviour
{
    public void GetEKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetEKeyEvent();
        }
    }

    protected abstract void GetEKeyEvent();
}
