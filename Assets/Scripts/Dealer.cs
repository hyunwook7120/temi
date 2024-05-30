using UnityEngine;
using System.Collections.Generic;

public class Dealer : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    public IReadOnlyList<Card> Deck => deck.AsReadOnly(); // 읽기 전용으로 덱 제공

    // 덱 초기화
    public void InitializeDeck()
    {
        for (int j = 1; j <= 10; j++)
        {
            GameObject cardPrefab = CardManager.Instance.GetCardPrefab(j);
            if (cardPrefab != null)
            {
                GameObject cardObject = Instantiate(cardPrefab);
                Card card = cardObject.GetComponent<Card>();
                card.Initialize(j);
                deck.Add(card);
            }
            else
            {
                Debug.LogError("Card prefab not found for value: " + j);
            }
        }
        ShuffleDeck();
    }

    // 덱 셔플 메서드
    private void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // 카드 배분 메서드
    public Card DealCard()
    {
        if (deck.Count == 0) return null;
        Card dealtCard = deck[0];
        deck.RemoveAt(0);
        return dealtCard;
    }
}
