using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムパネルを制御
/// </summary>
public class ItemPanel : MonoBehaviour
{
    RectTransform rectTransform;

    //表示されているか
    bool isView;

    //表示・非表示のスピード
    [SerializeField]
    float speed;

    [SerializeField]
    List<Image> slotImages = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //アイテムスロットのアルファ値を0にする
        for (int i = 0; i < slotImages.Count; i++)
        {
            slotImages[i].color = new Color(slotImages[i].color.r, slotImages[i].color.g,
                slotImages[i].color.b, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Xキーでアイテムパネルの表示・非表示
        if (Input.GetKeyDown(KeyCode.X))
        {
            isView = !isView;
        }

        if (isView)
        {
            rectTransform.transform.localPosition += Vector3.right * Time.deltaTime * speed;

            if (rectTransform.transform.localPosition.x >= -350)
            {
                rectTransform.transform.localPosition = new Vector3(-350, rectTransform.transform.localPosition.y, rectTransform
                    .transform.localPosition.z);
            }

        }

        else
        {
            rectTransform.transform.localPosition += Vector3.right * Time.deltaTime * -speed;

            if (rectTransform.transform.localPosition.x <= -450)
            {
                rectTransform.transform.localPosition = new Vector3(-450, rectTransform.transform.localPosition.y, rectTransform
                    .transform.localPosition.z);
            }
        }
    }

    /// <summary>
    /// アイテムの画像をアイテム欄にセットするメソッド
    /// </summary>
    /// <param name="item"></param>
    public void SetItemImage(Sprite sprite)
    {
        for(int i = 0; i < slotImages.Count; i++)
        {
            if (slotImages[i].sprite == null)
            {
                slotImages[i].sprite = sprite;

                //アルファ値を1
                slotImages[i].color += new Color(0.0f, 0.0f, 0.0f, 1.0f);

                return;
            }
        }
    }

    /// <summary>
    /// アイテムの画像をアイテム欄から削除するメソッド
    /// </summary>
    /// <param name="item"></param>
    public void RemoveImage(Sprite sprite)
    {
        for (int i = 0; i < slotImages.Count; i++)
        {
            if(slotImages[i].sprite == sprite)
            {
                slotImages[i].sprite = null;

                //アルファ値を0
                slotImages[i].color -= new Color(0.0f, 0.0f, 0.0f, 1.0f);

                return;
            }
        }
    }
}
