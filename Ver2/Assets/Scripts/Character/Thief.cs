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
        // 만일 승리 시
        if(transform.parent.GetComponent<Node>() != null)
        {
            if(transform.parent.GetComponent<Node>().isExitNode)
            {
                GameManager.Instance.EndStage();
            }
        }

        // 턴 기능
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
                        // 탈출구가 있다면 우선 반환
                        return node;
                    }

                    if (node.IsMoveable)
                    {
                        // 이동 가능한 노드만 추가
                        moveableNodes.Add(node);
                    }
                }

                if (moveableNodes.Count == 0)
                {
                    // 이동 가능한 노드가 없는 경우
                    return null;
                }

                // 이동 가능한 노드 중에서 랜덤 선택
                var randomIndex = Random.Range(0, moveableNodes.Count);
                return moveableNodes[randomIndex];
            }
        }
        return null;
    }

}
