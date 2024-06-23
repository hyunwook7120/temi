using UnityEngine;

public class Card : MonoBehaviour
{
     public int value; // 카드의 값
     public string suit; // 카드의 슈트(종류)

     // 카드 초기화 메서드
     public void Initialize(int cardValue, string cardSuit)
     {
         value = cardValue;
         suit = cardSuit;
         UpdateCardSprite();
     }

     // 카드 스프라이트 업데이트 메서드
     private void UpdateCardSprite()
    {
        GameObject cardPrefab = CardManager.Instance.GetCardPrefab(value);
        if (cardPrefab != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component is missing on the card.");
                return;
            }
        
            SpriteRenderer prefabSpriteRenderer = cardPrefab.GetComponent<SpriteRenderer>();
            if (prefabSpriteRenderer != null)
            {
                spriteRenderer.sprite = prefabSpriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer component is missing on the prefab.");
            }
        }
        else
        {
            Debug.LogError("Card prefab not found for value: " + value + ", suit: " + suit);
        }
    }
}

