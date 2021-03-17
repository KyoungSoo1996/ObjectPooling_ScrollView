using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class scrollViewControl : MonoBehaviour
{
    int startIndex, EndIndex, viewStartIndex, viewPrevIndex, viewEndIndex;
    int intervalIndex;
    [SerializeField] int marginCount;
    [SerializeField] private Transform transformContentPrefabs;
    [SerializeField] private Transform transformView;
    [SerializeField] private Transform transformViewport;

    List<Transform> transformContents = new List<Transform>();

    //
    List<ColorButtonContent> colorButtonContents = new List<ColorButtonContent>();
    //

    int contentsCount, dataCount;
    private RectTransform rectViewport;
    private RectTransform rectScrollView;
    private RectTransform rectContentPrefabs;

    private void Awake()
    {
        //
        for (int i = 0; i < 100; i++)
        {
            Color temp = new Color(Random.value, Random.value, Random.value);
            ColorButtonContent tempcolorButtonContnet = new ColorButtonContent(i, temp, () =>
            {
                Debug.Log(" 무슨 색 일까요? : " + temp);
            });
            colorButtonContents.Add(tempcolorButtonContnet);
        }
        dataCount = colorButtonContents.Count;
        //
    }

    private void Start()
    {
        InitScrollView();
        DragScrollViewControl();
    }

    public void OnDragScrollView()
    {
        viewStartIndex = (int)((rectScrollView.localPosition.y - 400) / (rectContentPrefabs.rect.height + spacing));
        viewEndIndex = viewStartIndex + intervalIndex;

        if (viewStartIndex != viewPrevIndex)
        {
            DragScrollViewControl();
        }
    }

    private void InitScrollView()
    {
        rectContentPrefabs = transformContentPrefabs.GetComponent<RectTransform>();
        rectScrollView = transformView.GetComponent<RectTransform>();
        rectViewport = transformViewport.GetComponent<RectTransform>();
        intervalIndex = (int)(rectViewport.rect.height / rectContentPrefabs.rect.height) + 1;
        contentsCount = intervalIndex + marginCount * 2;
        for (int i = 0; i < contentsCount; i++)
        {
            transformContents.Add(Instantiate(rectContentPrefabs, Vector3.one, Quaternion.identity, transformView));
        }
        SetViewRectSize(dataCount);
        EndIndex = contentsCount > dataCount ? dataCount : contentsCount;
    }

    float contentHeightSize;
    float spacing;

    private void SetViewRectSize(int _count)
    {
        spacing = transformView.GetComponent<VerticalLayoutGroup>().spacing;
        contentHeightSize = spacing + rectContentPrefabs.rect.height;
        rectScrollView.sizeDelta = new Vector2(0, (contentHeightSize * _count) + spacing);
        rectScrollView.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < contentsCount; i++)
        {
            transformContents[i % contentsCount].GetComponent<contentControl>().SetData(colorButtonContents[i]);
        }
    }

    private void DragScrollViewControl()
    {
        for (int i = startIndex; i < EndIndex; i++)
        {
            if (viewStartIndex <= i && i <= viewEndIndex)
            {
                transformContents[i % contentsCount].GetComponent<contentControl>().SetData(colorButtonContents[i]);
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
