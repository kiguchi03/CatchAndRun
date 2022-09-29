using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//赤いバラのスポーンを制御
public class RedRoseSpanwer : MonoBehaviour
{
    [SerializeField]
    GameObject redRose;

    [SerializeField]
    FlagData redRoseFlag;

    bool isSpawned;

    private void Start()
    {
        redRoseFlag.GetSetIsBool = false;

        redRose.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (redRoseFlag.GetSetIsBool && !isSpawned)
        {
            redRose.SetActive(true);

            isSpawned = true;
        }    
    }
}
