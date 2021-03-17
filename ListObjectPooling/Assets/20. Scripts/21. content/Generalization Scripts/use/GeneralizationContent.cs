using UnityEngine;
using UnityEngine.UI;

public class GeneralizationContent : MonoBehaviour, IContentUIControl<GeneralizationData>
{
    [SerializeField] private Text txt_Number;
    [SerializeField] private Button btn_Action;
    [SerializeField] private Image image;
    [SerializeField] CanvasGroup canvasGroup;


    public void InitData()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        txt_Number.text = "";
        btn_Action.onClick.RemoveAllListeners();
    }

    public void SetData(GeneralizationData _t)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        txt_Number.text = _t.number.ToString();
        btn_Action.onClick.AddListener(delegate { _t.buttonEvent(); });
        image.color = _t.color;
    }
}
