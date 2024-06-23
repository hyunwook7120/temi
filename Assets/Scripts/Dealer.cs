using UnityEngine;
using System.Collections.Generic;
using System;

public class Dealer : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    public IReadOnlyList<Card> Deck => deck.AsReadOnly(); // 읽기 전용으로 덱 제공

    // 덱 초기화
    public void InitializeDeck()
    {
        try
        {
            deck.Clear(); // 기존 덱 클리어
            for (int j = 1; j <= 10; j++) // 가정: 카드는 1부터 10까지
            {
                GameObject cardPrefab = CardManager.Instance.GetCardPrefab(j);
                if (cardPrefab != null)
                {
                    GameObject cardObject = Instantiate(cardPrefab);
                    Card card = cardObject.GetComponent<Card>();
                    card.Initialize(j, ""); // 슈트는 ""로 처리
                    deck.Add(card);
                }
                else
                {
                    Debug.LogError("Card prefab not found for value: " + j);
                }
            }
            ShuffleDeck();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error initializing the deck: " + ex.Message);
        }
    }
    
    // 덱 셔플 메서드
    public void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

<<<<<<< HEAD
    // 카드 배분 메서드
=======
     // 카드 배분 메서드
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    public Card DealCard()
    {
        if (deck.Count == 0)
        {
            Debug.LogError("No cards left in the deck to deal.");
            return null;
        }

        Card dealtCard = deck[0];
        deck.RemoveAt(0);

        // 카드의 활성화 상태를 로그로 출력
        if (dealtCard.gameObject.activeInHierarchy)
        {
            Debug.Log("Dealing an active card: " + dealtCard.value + " of " + dealtCard.suit);
        }
        else
        {
            Debug.Log("Dealing an inactive card, activating it: " + dealtCard.value + " of " + dealtCard.suit);
            dealtCard.gameObject.SetActive(true); // 활성화 상태가 아니면 활성화
        }

        return dealtCard;
    }
<<<<<<< HEAD
}
=======

}
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
