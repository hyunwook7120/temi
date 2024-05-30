using UnityEngine;
using System.Collections.Generic;

public class Dealer : MonoBehaviour
{
    private List<Card> deck = new List<Card>();
    public IReadOnlyList<Card> Deck => deck.AsReadOnly(); // �б� �������� �� ����

    // �� �ʱ�ȭ
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

    // �� ���� �޼���
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

    // ī�� ��� �޼���
    public Card DealCard()
    {
        if (deck.Count == 0) return null;
        Card dealtCard = deck[0];
        deck.RemoveAt(0);
        return dealtCard;
    }
}
