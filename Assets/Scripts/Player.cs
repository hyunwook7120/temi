using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    public int score;
    public bool hasPassed;
    public Card card;

    // �ʱ�ȭ
    public void Initialize(int id)
    {
        playerID = id;
        score = 0;
        hasPassed = false;
    }

    // ī�� ����
    public void SetCard(Card newCard)
    {
        card = newCard;
        card.gameObject.SetActive(true);
    }

    // ���� ������Ʈ
    public void UpdateScore(int points)
    {
        score += points;
    }

    // �н� ���� ����
    public void Pass()
    {
        hasPassed = true;
    }
}
