using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WhoisTurn { Wait, Police, Thief}
public enum StageList {None ,Stage1, Stage2, Stage3, Stage4}

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    // 클릭 상태 저장
    public bool isClick = false;
    public Police choicePolice = null;

    // 경로 저장
    public List<Node> nowNodes;

    // 턴 확인 - enum으로 관리
    public WhoisTurn whoTurn;
    public StageList nowStage;

    // 스테이지 관리
    public Transform canvasTransform;
    public List<GameObject> stages = new List<GameObject>();
    public List<Button> stageButtons;

    public GameObject stageObj;


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            canvasTransform = GameObject.Find("Canvas").transform;
            for(int i = 0; i < canvasTransform.childCount; i++)
            {
                stages.Add(canvasTransform.GetChild(i).gameObject);
            }
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 오브젝트가 파괴되지 않도록 설정
            OFFAllStage();
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 다른 인스턴스가 존재하면 현재 인스턴스를 파괴
        }
    }

    public void ChoiceButton(int index)
    {
        switch (index)
        {
            case 1:
                nowStage = StageList.Stage1;
                break;
            case 2:
                nowStage = StageList.Stage2;
                break;
            case 3:
                nowStage = StageList.Stage3;
                break;
            case 4:
                nowStage = StageList.Stage4;
                break;
        }

        SelectStage(nowStage);
    }

    public void SelectStage(StageList choiceStage)
    {
        switch(choiceStage)
        {
            case StageList.Stage1:
                nowStage = StageList.Stage1;
                ONStage(0);
                break;
            case StageList.Stage2:
                nowStage = StageList.Stage2;
                ONStage(1);
                break;
            case StageList.Stage3:
                nowStage = StageList.Stage3;
                ONStage(2);
                break;
            case StageList.Stage4:
                nowStage = StageList.Stage4;
                ONStage(3);
                break;
        }

        StartStage();
    }

    public void StartStage()
    {
        whoTurn = WhoisTurn.Thief;
    }

    public void EndStage()
    {
        OFFAllStage();
        ResetStage();
        switch (nowStage)
        {
            case StageList.Stage1:
                stageButtons[0].enabled = false;
                stageButtons[0].GetComponent<Image>().color = Color.black;
                break;
            case StageList.Stage2:
                stageButtons[1].enabled = false;
                stageButtons[1].GetComponent<Image>().color = Color.black;
                break;
            case StageList.Stage3:
                stageButtons[2].enabled = false;
                stageButtons[2].GetComponent<Image>().color = Color.black;
                break;
            case StageList.Stage4:
                stageButtons[3].enabled = false;
                stageButtons[3].GetComponent<Image>().color = Color.black;
                break;
        }
        stageObj.SetActive(true);
        nowStage = StageList.None;
    }

    public void ONStage(int stageIndex)
    {
        stageObj.SetActive(false);
        stages[stageIndex].gameObject.SetActive(true);
    }

    public void OFFAllStage()
    {
        for(int i = 0; i < stages.Count; i++)
        {
            if (stages[i].GetComponent<Stage>() != null)
            {
                stages[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetStage()
    {

    }
}
