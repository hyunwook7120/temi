using UnityEngine;

public class Card : MonoBehaviour
{
    public int value; // 카드의 값

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // 스프라이트 렌더러 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 스프라이트 렌더러가 없으면 에러 로그를 출력합니다.
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on " + gameObject.name);
        }
    }

    // 카드 초기화 메서드
    public void Initialize(int cardValue)
    {
        value = cardValue;
        UpdateCardSprite();
    }

    // 카드 스프라이트 업데이트 메서드
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