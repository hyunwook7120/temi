using UnityEngine;

public class Card : MonoBehaviour
{
    public int value; // ī���� ��

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // ��������Ʈ ������ ������Ʈ�� �����ɴϴ�.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��������Ʈ �������� ������ ���� �α׸� ����մϴ�.
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on " + gameObject.name);
        }
    }

    // ī�� �ʱ�ȭ �޼���
    public void Initialize(int cardValue)
    {
        value = cardValue;
        UpdateCardSprite();
    }

    // ī�� ��������Ʈ ������Ʈ �޼���
    private void UpdateCardSprite()
    {
        if (spriteRenderer != null)
        {
            GameObject cardPrefab = CardManager.Instance.GetCardPrefab(value);
            if (cardPrefab != null)
            {
                SpriteRenderer cardSpriteRenderer = cardPrefab.GetComponent<SpriteRenderer>();
                if (cardSpriteRenderer != null)
                {
                    spriteRenderer.sprite = cardSpriteRenderer.sprite;
                }
                else
                {
                    Debug.LogError("SpriteRenderer is missing on the card prefab: " + cardPrefab.name);
                }
            }
            else
            {
                Debug.LogError("Card prefab not found for value: " + value);
            }
        }
    }
}
