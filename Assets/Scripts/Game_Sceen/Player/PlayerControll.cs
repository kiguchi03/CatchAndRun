using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きを制御
/// </summary>
public class PlayerControll : MonoBehaviour
{
    [Header("移動スピード")]
    [SerializeField]
    float moveSpeed = 0.5f;

    [Header("移動を許可")]
    [SerializeField]
    bool isMove = true;

    [Tooltip("足音のSE")]
    [SerializeField]
    AudioClip stepClip;

    GameObject _camera;

    Rigidbody rid;

    Animator animator;


    Vector3 playerMove;

    //上下キーの入力値
    float InputVertical;

    //左右キーの入力値
    float InputHorizontal;


    public bool GetSetIsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        rid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();

        animator.SetFloat("Speed", rid.velocity.magnitude);
    }

    /// <summary>
    /// 移動のメソッド
    /// </summary>
    private void Moving()
    {
        if (!isMove)
        {
            rid.velocity = Vector3.zero;
            return;
        }

        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxis("Vertical");


        playerMove = _camera.transform.forward * InputVertical * moveSpeed
            + _camera.transform.right * InputHorizontal * moveSpeed;

        rid.velocity = playerMove;
    }


    public void AnimEvent_Step()
    {
        SoundManager.instance.PlaySE(stepClip);
    }
}
