using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Thief : MonoBehaviour
{
    private void Update()
    {
        // ���� �¸� ��
        if(transform.parent.GetComponent<Node>() != null)
        {
            if(transform.parent.GetComponent<Node>().isExitNode)
            {
                GameManager.Instance.EndStage();
            }
        }

        // �� ���
        if(GameManager.Instance.whoTurn == WhoisTurn.Thief)
        {
            Turn();
        }
    }

    public void Turn()
    {
        Node node = Search();
        if(node != null)
        {
            Move(node);
            GameManager.Instance.whoTurn = WhoisTurn.Police;
        } else
        {
            GameManager.Instance.EndStage();
        }
    }

    public void Move(Node randomNode)
    {
        transform.parent = randomNode.transform;
        GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    public Node Search()
    {
        var parent = transform.parent;
        if (parent != null)
        {
            var parentNode = parent.GetComponent<Node>();
            if (parentNode != null)
            {
                var nodes = parentNode.linkedNode;
                var moveableNodes = new List<Node>();

                foreach (var node in nodes)
                {
                    if (node.isExitNode)
                    {
                        // Ż�ⱸ�� �ִٸ� �켱 ��ȯ
                        return node;
                    }

                    if (node.IsMoveable)
                    {
                        // �̵� ������ ��常 �߰�
                        moveableNodes.Add(node);
                    }
                }

                if (moveableNodes.Count == 0)
                {
                    // �̵� ������ ��尡 ���� ���
                    return null;
                }

                // �̵� ������ ��� �߿��� ���� ����
                var randomIndex = Random.Range(0, moveableNodes.Count);
                return moveableNodes[randomIndex];
            }
        }
        return null;
    }

}
