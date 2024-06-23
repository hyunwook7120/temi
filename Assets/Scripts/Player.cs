using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    public int score;
    public bool hasPassed;
    public Card card;

    // 초기화
    public void Initialize(int id)
    {
        playerID = id;
        score = 0;
        hasPassed = false;
    }

    // 카드 설정
    public void SetCard(Card newCard)
    {
        card = newCard;
        card.gameObject.SetActive(true);
    }

    // 점수 업데이트
    public void UpdateScore(int points)
    {
        score += points;
    }

    // 패스 상태 설정
    public void Pass()
    {
        hasPassed = true;
    }
}