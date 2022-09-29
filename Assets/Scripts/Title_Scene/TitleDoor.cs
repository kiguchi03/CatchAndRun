using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルでの揺れるドアを制御
public class TitleDoor : MonoBehaviour
{
    private Quaternion startAngle;

    [SerializeField]
    private float angle;

    private float sec;

    [SerializeField]
    private float rotateSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        startAngle = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;

        transform.rotation = Quaternion.AngleAxis(Mathf.Sin(sec * rotateSpeed) * angle
            ,Vector3.up) * startAngle;
    }
}
