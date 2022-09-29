using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テレビのイベント、マテリアル、オーディオを制御
/// </summary>
public class TvScreen : GetInput
{
    [SerializeField]
    MessageControll messageControll;

    [SerializeField]
    RoseSpawn_GM roseSpawn_GM;

    [SerializeField]
    SpawnerControll spawnerControll;

    Renderer matRender;

    [SerializeField]
    AudioClip noise;

    [Header("ブラックマテリアル")]
    [SerializeField]
    Material BlackScreen;

    [Header("ノイズマテリアル")]
    [SerializeField]
    Material NoiseScreen;

    [Header("最初のメッセージ")]
    [SerializeField]
    string firstMessage;

    [Header("遅延秒数")]
    [SerializeField]
    float delayTime = 2.0f;

    //表示するメッセージ
    string message;

    //テレビスクリーンをノイズにするか
    bool isNoisy;

    //基準となる音量
    float baseVolume;

    //最初のイベントが終わってるか
    static bool isFirstEvent;


    public bool GetSetIsNoisy
    {
        set { isNoisy = value; }
        get { return isNoisy; }
    }


    public bool GetSetIsFirstEvent
    {
        get { return isFirstEvent; }
        set { isFirstEvent = value; }
    }

    public string GetSetMessage
    {
        get { return message; }
        set { message = value; }
    }


    private void Awake()
    {
        if (!isFirstEvent) TvFirstEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        matRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenCon();
    }

    /// <summary>
    /// ゲーム開始直後テレビをノイズにし、敵のスポーンを止めるメソッド
    /// </summary>
    private void TvFirstEvent()
    {
        isNoisy = true;

        message = firstMessage;

        spawnerControll.GetSetNoSpawn = true;

        isFirstEvent = true;
    }

    /// <summary>
    /// メッセージを表示し、時間を停止させるメソッド
    /// </summary>
    protected override void GetEKeyEvent()
    {
        if (isNoisy && !InputManager.instance.GetSetNoInput)
        {
            messageControll.ViewMessage(message);

            TimeManager.instance.StopTime();

            GetSetIsNoisy = false;

            StartCoroutine(NotInput(delayTime));
        }   
    }

    /// <summary>
    /// テレビスクリーンのマテリアルをコントロールするメソッド
    /// </summary>
    private void ScreenCon()
    {
        //テレビスクリーンをノイズマテリアルにする
        if (isNoisy && matRender.sharedMaterial != NoiseScreen)
        {
            matRender.material = NoiseScreen;

            SoundManager.instance.PlayBGM(noise);
        }

        //メッセージ表示中にEキーを押すとメッセージを閉じ、テレビスクリーンをブラックマテリアルにし、敵のスポーンを開始させる
        if (!isNoisy  && Input.GetKeyUp(KeyCode.E) && !InputManager.instance.GetSetNoInput && matRender.sharedMaterial != BlackScreen)
        {
            TimeManager.instance.StartTime();

            SoundManager.instance.StopBGM();

            matRender.material = BlackScreen;

            spawnerControll.GetSetNoSpawn = false;

            messageControll.CloseMessage();
        }
    }

    /// <summary>
    /// メッセージを表示し、時間を停止させるメソッド
    /// </summary>
    public void TvRoseEvent()
    {
        if (isNoisy && Input.GetKeyUp(KeyCode.E) && !InputManager.instance.GetSetNoInput)
        {
            messageControll.ViewMessage(message);

            TimeManager.instance.StopTime();

            GetSetIsNoisy = false;

            StartCoroutine(NotInput(delayTime));
        }
    }

    /// <summary>
    /// キー連打防止用メソッド
    /// </summary>
    /// <returns></returns>
    private IEnumerator NotInput(float sec)
    {
        InputManager.instance.GetSetNoInput = true;

        yield return new WaitForSecondsRealtime(sec);

        InputManager.instance.GetSetNoInput = false;
    }
}
