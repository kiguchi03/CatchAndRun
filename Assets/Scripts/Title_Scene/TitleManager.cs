using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//タイトルでのUI、シーン遷移を制御
public class TitleManager : MonoBehaviour
{
    [SerializeField]
    EventSystem eventSystem;

    [Header("タイトルBGM")]
    [SerializeField]
    AudioClip titleBGM;

    [Header("最初に選択されるボタン")]
    [SerializeField]
    Button firstSelectButton;

    [Header("シーン移動の遅延秒数")]
    [SerializeField]
    float sceneMoveTime;

    [Header("選択されたくないUIオブジェクト")]
    [SerializeField]
    List<GameObject> notSelected = new List<GameObject>();

    //直後に選択されていたUIオブジェクト
    GameObject previous;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        firstSelectButton.Select();

        FadeManager.instance.FadeIn(0.5f);

        SoundManager.instance.PlayBGM(titleBGM);
    }

    private void Update()
    {
        //MenuPanelのスライダーをマウスで選択してもボタンからフォーカスが外れないようにする
        if ((previous != eventSystem.currentSelectedGameObject || previous == null) &&
            !notSelected.Contains(eventSystem.currentSelectedGameObject) && eventSystem.currentSelectedGameObject != null)
        {
            previous = eventSystem.currentSelectedGameObject;
        }

        if (eventSystem.currentSelectedGameObject == null || notSelected.Contains(eventSystem.currentSelectedGameObject))
        {
            EventSystem.current.SetSelectedGameObject(previous);
        }
    }

    /// <summary>
    /// GameSceneに遷移するメソッド
    /// </summary>
    public void StartButton()
    {
        if (FadeManager.instance.GetNotInput) return;

        FadeManager.instance.FadeOut(1.0f);

        StartCoroutine(DelaySceneLoad(sceneMoveTime, 1));
    }

    /// <summary>
    /// シーン遷移メソッド
    /// </summary>
    /// <param name="sec"></param>
    /// <param name="sceneValue"></param>
    /// <returns></returns>
    private IEnumerator DelaySceneLoad(float sec,int sceneValue)
    {
        yield return new WaitForSeconds(sec);

        SceneManager.LoadScene(sceneValue);
    }
}
