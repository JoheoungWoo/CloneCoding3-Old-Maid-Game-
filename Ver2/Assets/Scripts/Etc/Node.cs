using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler
{
    public Color orignalColor { get; private set; }

    // false인 경우 이동 불가능함
    private bool isMoveable;
    public bool IsMoveable => isMoveable;

    // 해당지점이 끝 지점인지
    public bool isExitNode;
    // 이동가능한 노드 확인
    public List<Node> linkedNode;

    private void Awake()
    {
        orignalColor = GetComponent<Image>().color;
    }

    private void Update()
    {
        // 현재 Transform 객체의 자식이 있는지 확인
        if (transform.childCount > 0)
        {
            isMoveable = false;
        } else
        {
            isMoveable = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsNodeInNowNodes(this))
        {
            GameManager.Instance.choicePolice.transform.SetParent(transform);

            foreach (var node in GameManager.Instance.nowNodes)
            {
                Image nodeImage = node.GetComponent<Image>();
                // 원래 색상으로 복구
                nodeImage.color = node.orignalColor;
            }

            // 위치 고정 및 정보 초기화
            GameManager.Instance.choicePolice.GetComponent<Image>().color = orignalColor;
            GameManager.Instance.choicePolice.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            GameManager.Instance.nowNodes = null;
            GameManager.Instance.choicePolice = null;
            GameManager.Instance.isClick = false;

            GameManager.Instance.whoTurn = WhoisTurn.Thief;

        } else
        {
            Debug.Log("비활성화 상태임");
        }
    }

    // 특정 Node가 nowNodes 리스트에 포함되어 있는지 확인
    public bool IsNodeInNowNodes(Node nodeToCheck)
    {
        // GameManager의 nowNodes 리스트를 가져옵니다.
        List<Node> nowNodes = GameManager.Instance.nowNodes;

        // 리스트에서 Node가 존재하는지 확인합니다.
        if(nowNodes != null)
        {
            return nowNodes.Contains(nodeToCheck) && GetComponent<Image>().color != orignalColor;
        } else
        {
            return false;
        }
    }

}
