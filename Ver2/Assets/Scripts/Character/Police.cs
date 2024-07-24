using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Police : MonoBehaviour, IPointerClickHandler
{
    private Image imageComponent;
    private Color clickColor = Color.blue; // 클릭 시 변경할 색상
    private Color originalColor; // 원래 색상

    private void Start()
    {
        // Image 컴포넌트를 가져옵니다.
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("Image component not found!");
            return;
        }

        // 원래 색상 저장
        originalColor = imageComponent.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.whoTurn != WhoisTurn.Police)
            return;

        // GameManager의 isClick 상태 확인
        if (GameManager.Instance.isClick)
        {
            // isClick이 true일 경우: 색상을 원래 색상으로 복구하고 상태를 false로 변경
            if (imageComponent != null && imageComponent.color == clickColor)
            {
                imageComponent.color = originalColor;
                GameManager.Instance.isClick = false;
                TraceStage();
            }
        }
        else
        {
            // isClick이 false일 경우: 색상을 클릭 색상으로 변경하고 상태를 true로 설정
            if (imageComponent != null)
            {
                imageComponent.color = clickColor;
                GameManager.Instance.isClick = true;
                TraceStage();
            }
        }
    }

    public void TraceStage()
    {
        var parent = transform.parent;

        // 부모가 Node 컴포넌트를 가지고 있는지 확인
        var parentNode = parent.GetComponent<Node>();
        if (parentNode != null)
        {
            var linkedNodes = parentNode.linkedNode;
            GameManager.Instance.nowNodes = linkedNodes;

            foreach (var node in linkedNodes)
            {
                if (IsMoveable(node))
                {
                    Image nodeImage = node.GetComponent<Image>();
                    if (nodeImage != null)
                    {
                        // 상태에 따라 색상 변경 또는 복구
                        if (GameManager.Instance.isClick)
                        {
                            // 색상을 클릭 색상으로 변경
                            nodeImage.color = Color.green;
                            GameManager.Instance.choicePolice = this;
                        }
                        else
                        {
                            // 원래 색상으로 복구
                            nodeImage.color = node.orignalColor;
                            GameManager.Instance.nowNodes = null;
                            GameManager.Instance.choicePolice = null;

                        }
                    }
                }
            }
        }
    }

    public bool IsMoveable(Node node)
    {
        return node.IsMoveable;
    }
}
