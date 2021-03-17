using UnityEngine;
using System.Collections.Generic;

public class GeneralizationScrollView : AbstractScrollViewControl<GeneralizationContent, GeneralizationData>
{
    List<GeneralizationData> generalizationDatas;

    private void Awake()
    {
        generalizationDatas = new List<GeneralizationData>();
        for (int i = 0; i < 100; i++)
        {
            Color temp = new Color(Random.value, Random.value, Random.value);
            GeneralizationData generalizationData = new GeneralizationData(i, temp, () =>
            {
                Debug.Log(" 무슨 색 일까요? : " + temp);
            });
            generalizationDatas.Add(generalizationData);
        }
        Init(generalizationDatas);
    }

}
