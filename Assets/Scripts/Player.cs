using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    public int allChips; // 처음에 모든 칩을 10으로 설정
    public int bettingChips; // 기본 베팅칩 1로 설정
    public bool hasPassed;
    public Card card;

    // 초기화
    public void Initialize(int id)
    {
        playerID = id;
        bettingChips = 1; // 기본 베팅칩 1로 설정
        allChips = 10; // 모든 칩을 10으로 초기화
        hasPassed = false;
    }

    // 카드 설정
    public void SetCard(Card newCard)
    {
        if (newCard != null)
        {
            card = newCard;
            card.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("New card is null.");
        }
    }

    // 점수 업데이트
    public void UpdateScore(int points)
    {
        if (allChips >= points)
        {
            bettingChips += points;
            allChips -= points;
        }
    }

    // 패스 상태 설정
    public void Pass()
    {
        hasPassed = true;
    }
}
