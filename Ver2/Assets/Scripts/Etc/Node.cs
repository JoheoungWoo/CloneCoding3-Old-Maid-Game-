using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler
{
    public Color orignalColor { get; private set; }

    // false�� ��� �̵� �Ұ�����
    private bool isMoveable;
    public bool IsMoveable => isMoveable;

    // �ش������� �� ��������
    public bool isExitNode;
    // �̵������� ��� Ȯ��
    public List<Node> linkedNode;

    private void Awake()
    {
        orignalColor = GetComponent<Image>().color;
    }

    private void Update()
    {
        // ���� Transform ��ü�� �ڽ��� �ִ��� Ȯ��
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
                // ���� �������� ����
                nodeImage.color = node.orignalColor;
            }

            // ��ġ ���� �� ���� �ʱ�ȭ
            GameManager.Instance.choicePolice.GetComponent<Image>().color = orignalColor;
            GameManager.Instance.choicePolice.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            GameManager.Instance.nowNodes = null;
            GameManager.Instance.choicePolice = null;
            GameManager.Instance.isClick = false;

            GameManager.Instance.whoTurn = WhoisTurn.Thief;

        } else
        {
            Debug.Log("��Ȱ��ȭ ������");
        }
    }

    // Ư�� Node�� nowNodes ����Ʈ�� ���ԵǾ� �ִ��� Ȯ��
    public bool IsNodeInNowNodes(Node nodeToCheck)
    {
        // GameManager�� nowNodes ����Ʈ�� �����ɴϴ�.
        List<Node> nowNodes = GameManager.Instance.nowNodes;

        // ����Ʈ���� Node�� �����ϴ��� Ȯ���մϴ�.
        if(nowNodes != null)
        {
            return nowNodes.Contains(nodeToCheck) && GetComponent<Image>().color != orignalColor;
        } else
        {
            return false;
        }
    }

}
