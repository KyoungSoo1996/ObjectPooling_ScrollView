using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractScrollViewControl<TUI, TData> : MonoBehaviour where TUI : IContentUIControl<TData> where TData : class
{
    [Header("hide objects")]
    [SerializeField] int marginCount;

    [Header("Content Object Prefabs")]
    [SerializeField] private Transform transformContentPrefabs = null;

    [Header("scrollView Content")]
    [SerializeField] private Transform transformView = null;

    [Header("scrollView Viewport")]
    [SerializeField] private Transform transformViewport = null;

    private RectTransform rectViewport = null;
    private RectTransform rectScrollView = null;
    private RectTransform rectContentPrefabs = null;

    // List View Index
    private int startIndex, endIndex;
    private int viewStartIndex, viewEndIndex, viewPrevIndex;
    private int intervalIndex;

    private int contentsCount, dataCount;
    private float contentHeightSize;
    private float spacing;

    protected List<Transform> transformContents = new List<Transform>();

    private List<TData> contentDatas { get; set; }

    public void Init()
    {
        InitScrollView();
    }

    public void SetData(List<TData> _Tdata)
    {
        ResetList();
        setContentData(_Tdata);
        SetScrollView();
    }

    public void OnDragScrollView()
    {
        viewStartIndex = (int)((rectScrollView.localPosition.y - (rectViewport.rect.height * 0.5f)) / (rectContentPrefabs.rect.height + spacing));
        viewEndIndex = viewStartIndex + contentsCount;
        if (viewStartIndex != viewPrevIndex)
        {
            DragScrollViewControl();
        }
    }

    private void ResetList()
    {
        for (int i = 0; i < transformContents.Count; i++)
        {
            Destroy(transformContents[i].gameObject);
        }
        transformContents.Clear();
    }

    private void setContentData(List<TData> _Tdata)
    {
        contentDatas = _Tdata;
        dataCount = contentDatas.Count;
    }

    private void InitScrollView()
    {
        rectContentPrefabs = transformContentPrefabs.GetComponent<RectTransform>();
        rectScrollView = transformView.GetComponent<RectTransform>();
        rectViewport = transformViewport.GetComponent<RectTransform>();
        intervalIndex = (int)(rectViewport.rect.height / rectContentPrefabs.rect.height) + 1;
        contentsCount = intervalIndex + marginCount * 2;
        spacing = transformView.GetComponent<VerticalLayoutGroup>().spacing;
        contentHeightSize = spacing + rectContentPrefabs.rect.height;
        rectScrollView.anchoredPosition = new Vector2(0, 0);
    }

    private void SetScrollView()
    {
        endIndex = contentsCount > dataCount ? dataCount : contentsCount;
        for (int i = 0; i < endIndex; i++)
        {
            transformContents.Add(MonoBehaviour.Instantiate(rectContentPrefabs, transformView));
        }
        rectScrollView.sizeDelta = new Vector2(rectScrollView.sizeDelta.x, (contentHeightSize * dataCount) + spacing);
        for (int i = 0; i < endIndex; i++)
        {
            transformContents[i % contentsCount].GetComponent<TUI>().InitData();
            transformContents[i % endIndex].GetComponent<TUI>().SetData(contentDatas[i]);
        }
    }

    private void DragScrollViewControl()
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            if (viewStartIndex <= i && i <= viewEndIndex)
            {
                transformContents[i % contentsCount].GetComponent<TUI>().InitData();
                transformContents[i % contentsCount].GetComponent<TUI>().SetData(contentDatas[i]);
            }
            RectTransform _rect = transformContents[i % contentsCount].GetComponent<RectTransform>();
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, -(i * contentHeightSize) - spacing);
        }
        if (viewPrevIndex <= viewStartIndex)
        {
            startIndex = Mathf.Clamp(viewStartIndex + 1, 0, dataCount);
            endIndex = Mathf.Clamp(viewEndIndex + 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
        else
        {
            startIndex = Mathf.Clamp(viewStartIndex - 1, 0, dataCount);
            endIndex = Mathf.Clamp(viewEndIndex - 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
    }
}
