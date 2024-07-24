using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Police : MonoBehaviour, IPointerClickHandler
{
    private Image imageComponent;
    private Color clickColor = Color.blue; // Ŭ�� �� ������ ����
    private Color originalColor; // ���� ����

    private void Start()
    {
        // Image ������Ʈ�� �����ɴϴ�.
        imageComponent = GetComponent<Image>();

        if (imageComponent == null)
        {
            Debug.LogError("Image component not found!");
            return;
        }

        // ���� ���� ����
        originalColor = imageComponent.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.whoTurn != WhoisTurn.Police)
            return;

        // GameManager�� isClick ���� Ȯ��
        if (GameManager.Instance.isClick)
        {
            // isClick�� true�� ���: ������ ���� �������� �����ϰ� ���¸� false�� ����
            if (imageComponent != null && imageComponent.color == clickColor)
            {
                imageComponent.color = originalColor;
                GameManager.Instance.isClick = false;
                TraceStage();
            }
        }
        else
        {
            // isClick�� false�� ���: ������ Ŭ�� �������� �����ϰ� ���¸� true�� ����
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

        // �θ� Node ������Ʈ�� ������ �ִ��� Ȯ��
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
                        // ���¿� ���� ���� ���� �Ǵ� ����
                        if (GameManager.Instance.isClick)
                        {
                            // ������ Ŭ�� �������� ����
                            nodeImage.color = Color.green;
                            GameManager.Instance.choicePolice = this;
                        }
                        else
                        {
                            // ���� �������� ����
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
