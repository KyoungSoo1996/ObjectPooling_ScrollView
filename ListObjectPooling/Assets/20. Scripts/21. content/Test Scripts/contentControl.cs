using UnityEngine;
using UnityEngine.UI;


public class contentControl : MonoBehaviour
{
    [SerializeField] Text text_number;
    [SerializeField] Image image_colorView;
    [SerializeField] Button btn_debugPrinter;
    [SerializeField] CanvasGroup canvasGroup;

    public void SetData(ColorButtonContent _contentData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        btn_debugPrinter.onClick.RemoveAllListeners();
        text_number.text = _contentData.number.ToString();
        image_colorView.color = _contentData.color;
        btn_debugPrinter.onClick.AddListener(delegate { _contentData.buttonEvent(); });
    }

    public void InitData()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        btn_debugPrinter.onClick.RemoveAllListeners();
    }
}
