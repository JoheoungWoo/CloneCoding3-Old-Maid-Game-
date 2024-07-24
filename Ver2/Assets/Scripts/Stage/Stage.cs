using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StageData
{
    public List<Node> columns = new List<Node>();

    public void AddRow(Node row)
    {
        columns.Add(row);
    }

    public List<Node> GetColumn()
    {
        return columns;
    }
}

public class Stage : MonoBehaviour
{
    public Transform myTransform;

    // 스테이지 저장
    public List<StageData> stages = new List<StageData>();

    private void Start()
    {
        myTransform = transform;
        var childCount = myTransform.childCount;
       
        for(int i = 0; i < childCount; i++)
        {
            var stage = new StageData();
            var rowCount = myTransform.GetChild(i).childCount;
            for (int j = 0; j < rowCount; j++)
            {
                var data = myTransform.GetChild(i).GetChild(j);
                stage.AddRow(data.GetComponent<Node>());
            }
            stages.Add(stage);
        }
    }
}
