using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードイン、フェードアウトを制御
/// </summary>
public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;


    Image FadePanel;

    float fadeInSpeed;

    float fadeOutSpeed;

    //フェードパネルのアルファ値
    float alpha;

    //フェードアウトするか
    bool isFadeOut;

    //フェードインするか
    bool isFadeIn;

    //入力を受付けないか
    bool notInput;

    //アルファ値をリセットしたか
    bool isReset;


    public bool GetIsFadeOut
    {
        get { return isFadeOut; }
    }

    public bool GetIsFadeIn
    {
        get { return isFadeIn; }
    }

    public bool GetNotInput
    {
        get { return notInput; }
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

    private void Start()
    {
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //フェードアウト
        if (isFadeOut)
        {
            FadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Image>();

            //フェードパネルのアルファ値を0にする
            if (!isReset)
            {
                FadePanel.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

                isReset = true;
            }

            if (!notInput) notInput = true;


            alpha += Time.deltaTime * fadeOutSpeed;

            //アルファ値が1以上になればフェードイン終了
            if (alpha >= 1.0f)
            {
                isFadeOut = false;

                isReset = false;

                alpha = 1.0f;

                notInput = false;
            }

            FadePanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }

        //フェードイン
        if (isFadeIn)
        {
            FadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Image>();

            //フェードパネルのアルファ値を1にする
            if (!isReset)
            {
                FadePanel.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

                isReset = true;
            }

            if (!notInput) notInput = true;


            alpha -= Time.deltaTime * fadeInSpeed;

            //アルファ値が0以下になればフェードイン終了
            if (alpha <= 0.0f)
            {
                isFadeIn = false;

                isReset = false;

                alpha = 0.0f;

                notInput = false;
            }

            FadePanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }

    public void FadeIn(float speed)
    {
        fadeInSpeed = speed;
        isFadeIn = true;
    }

    public void FadeOut(float speed)
    {
        fadeOutSpeed = speed;
        isFadeOut = true;
    }
}
