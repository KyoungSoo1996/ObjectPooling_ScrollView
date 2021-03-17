using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractScrollViewControl<TUI, TData> : MonoBehaviour where TUI : IContentUIControl<TData> where TData : class
{
    [Header("hide objects")]
    [SerializeField] int marginCount;

    [Header("Content Object Prefabs")]
    [SerializeField] private Transform transformContentPrefabs;

    [Header("scrollView Content")]
    [SerializeField] private Transform transformView;

    [Header("scrollView Viewport")]
    [SerializeField] private Transform transformViewport;

    // List View Index
    int startIndex { get; set; }
    int EndIndex { get; set; }
    int viewStartIndex { get; set; }
    int viewPrevIndex { get; set; }
    int viewEndIndex { get; set; }
    int intervalIndex { get; set; }
    //

    List<Transform> transformContents = new List<Transform>();

    List<TData> contentDatas { get; set; }


    public void setContentData(List<TData> _Tdata)
    {
        contentDatas = _Tdata;
        dataCount = contentDatas.Count;
    }

    int contentsCount, dataCount;
    private RectTransform rectViewport;
    private RectTransform rectScrollView;
    private RectTransform rectContentPrefabs;


    public void Init(List<TData> _Tdata)
    {
        setContentData(_Tdata);
        InitScrollView();
        DragScrollViewControl();
    }

    public void OnDragScrollView()
    {
        viewStartIndex = (int)((rectScrollView.localPosition.y - (rectViewport.rect.height * 0.5f)) / (rectContentPrefabs.rect.height + spacing));
        viewEndIndex = viewStartIndex + intervalIndex;

        if (viewStartIndex != viewPrevIndex)
        {
            DragScrollViewControl();
        }
    }

    public void InitScrollView()
    {
        rectContentPrefabs = transformContentPrefabs.GetComponent<RectTransform>();
        rectScrollView = transformView.GetComponent<RectTransform>();
        rectViewport = transformViewport.GetComponent<RectTransform>();
        intervalIndex = (int)(rectViewport.rect.height / rectContentPrefabs.rect.height) + 1;
        contentsCount = intervalIndex + marginCount * 2;
        for (int i = 0; i < contentsCount; i++)
        {
            transformContents.Add(MonoBehaviour.Instantiate(rectContentPrefabs, Vector3.one, Quaternion.identity, transformView));
        }
        SetViewRectSize(dataCount);
        EndIndex = contentsCount > dataCount ? dataCount : contentsCount;
    }

    float contentHeightSize;
    float spacing;

    public void SetViewRectSize(int _count)
    {
        spacing = transformView.GetComponent<VerticalLayoutGroup>().spacing;
        contentHeightSize = spacing + rectContentPrefabs.rect.height;
        rectScrollView.sizeDelta = new Vector2(0, (contentHeightSize * _count) + spacing);
        rectScrollView.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < contentsCount; i++)
        {
            transformContents[i % contentsCount].GetComponent<TUI>().SetData(contentDatas[i]);
        }
    }

    public void DragScrollViewControl()
    {
        for (int i = startIndex; i < EndIndex; i++)
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
            EndIndex = Mathf.Clamp(viewEndIndex + 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
        else
        {
            startIndex = Mathf.Clamp(viewStartIndex - 1, 0, dataCount);
            EndIndex = Mathf.Clamp(viewEndIndex - 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
    }
}
