using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メッセージボックスを制御
/// </summary>
public class MessageControll : MonoBehaviour
{
    [Header("メッセージボックス")]
    [SerializeField]
    GameObject messageBox;

    [Header("メッセージテキスト")]
    [SerializeField]
    Text messageTex;

    [Header("Eキーのテキストオブジェクト")]
    [SerializeField]
    GameObject EKeyImage;


    // Start is called before the first frame update
    void Start()
    {
        messageBox.SetActive(false);
    }

    /// <summary>
    /// 任意のメッセージを表示するメソッド
    /// </summary>
    /// <param name="tex">表示するメッセージ</param>
    public void ViewMessage(string tex)
    {
        messageBox.SetActive(true);

        messageTex.text = tex;

        InputManager.instance.GetSetNoInput = true;
    }

    /// <summary>
    /// メッセージボックスを非表示にするメソッド
    /// </summary>
    public void CloseMessage()
    {
        messageBox.SetActive(false);

        InputManager.instance.GetSetNoInput = false;
    }

    /// <summary>
    /// Eキーテキストを表示するメソッド
    /// </summary>
    public void EKey_SetTrue()
    {
        if (!EKeyImage.activeSelf)
        {
            EKeyImage.SetActive(true);
        }
    }

    /// <summary>
    /// Eキーテクストを非表示にするメソッド
    /// </summary>
    public void EKey_SetFalse()
    {
        if (EKeyImage.activeSelf)
        {
            EKeyImage.SetActive(false);
        }
    }
}
