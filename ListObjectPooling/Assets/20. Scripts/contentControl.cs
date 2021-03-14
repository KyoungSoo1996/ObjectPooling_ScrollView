using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class contentControl : MonoBehaviour
{
    [SerializeField] Text text_number;
    [SerializeField] Image image_colorView;
    [SerializeField] Button btn_debugPrinter;
    [SerializeField] CanvasGroup canvasGroup;

    public void SetData(ContentData _contentData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        text_number.text = _contentData.number.ToString();
        btn_debugPrinter.onClick.AddListener(delgate {btn_debugPrinter()});
    }

    public void InitData()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
