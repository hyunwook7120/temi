using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
<<<<<<< HEAD
    public int score;
=======
    public int allChips; // 처음에 모든 칩을 10으로 설정
    public int bettingChips; // 기본 베팅칩 1로 설정
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    public bool hasPassed;
    public Card card;

    // 초기화
    public void Initialize(int id)
    {
        playerID = id;
<<<<<<< HEAD
        score = 0;
=======
        bettingChips = 1; // 기본 베팅칩 1로 설정
        allChips = 10; // 모든 칩을 10으로 초기화
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        hasPassed = false;
    }

    // 카드 설정
    public void SetCard(Card newCard)
    {
<<<<<<< HEAD
        card = newCard;
        card.gameObject.SetActive(true);
=======
        if (newCard != null)
        {
            card = newCard;
            card.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("New card is null.");
        }
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    }

    // 점수 업데이트
    public void UpdateScore(int points)
    {
<<<<<<< HEAD
        score += points;
=======
        if (allChips >= points)
        {
            bettingChips += points;
            allChips -= points;
        }
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    }

    // 패스 상태 설정
    public void Pass()
    {
        hasPassed = true;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
