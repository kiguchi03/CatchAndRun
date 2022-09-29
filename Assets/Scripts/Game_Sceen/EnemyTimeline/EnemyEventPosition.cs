using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventPosition : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    // Update is called once per frame
    void FiexedUpdate()
    {
        transform.position = player.transform.position;
    }
}
