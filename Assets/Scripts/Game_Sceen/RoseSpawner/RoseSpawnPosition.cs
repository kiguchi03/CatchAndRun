using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseSpawnPosition : MonoBehaviour
{
    [SerializeField]
    private bool ThereIsRose;

    public int Value;

    bool isEnter;

    public bool isExit;

    bool isStay;

    public bool inPlayer;

    string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            isEnter = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            isStay = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            isExit = true;

        }
    }

    public bool GetInPlayer()
    {
        if (isExit)
        {
            inPlayer = false;
        }
        else if (isEnter || isStay)
        {
            inPlayer = true;
            Value++;
        }


        isEnter = false;
        isStay = false;
        isExit = false;



        return inPlayer;
    }

    public bool GetThereIsRose()
    {
        return ThereIsRose;
    }

    public void SetTrueThereIsRose()
    {
        ThereIsRose = true;
    }
}
