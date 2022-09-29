using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Flag Data",menuName ="CreateFlagData")]
public class FlagData : ScriptableObject
{
    [SerializeField]
    bool isBool = false;

    public bool GetSetIsBool
    {
        get { return isBool; }
        set { isBool = value; }
    }
}
