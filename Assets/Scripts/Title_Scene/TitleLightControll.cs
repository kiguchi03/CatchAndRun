using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルでの揺れるドアを制御
/// </summary>
public class TitleLightControll : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    float speed;

    [SerializeField]
    float intensity;

    Light _light;

    float sec = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sec += Time.deltaTime;

        _light.intensity = intensity * curve.Evaluate(sec * speed);
    }
}
