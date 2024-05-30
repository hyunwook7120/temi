using UnityEngine;
using UnityEngine.UI; // Button과 Image를 사용하기 위해 필요
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Image[] playerCards; // 각 플레이어의 카드 Image 배열
    public TextMeshProUGUI[] playerScores; // 각 플레이어의 점수 TextMeshProUGUI 배열
    public Button betButton;
    public Button passButton;

    // 플레이어 카드 UI 업데이트
    public void UpdatePlayerCardUI(int playerID, Card card)
    {
        if (playerID < playerCards.Length && playerCards[playerID] != null)
        {
            playerCards[playerID].sprite = card.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // 점수 UI 업데이트
    public void UpdateScoreUI(int playerID, int score)
    {
        if (playerID < playerScores.Length && playerScores[playerID] != null)
        {
            playerScores[playerID].text = "Score: " + score;
        }
    }

    // 베팅 및 패스 버튼 설정
    public void SetButtonCallbacks(UnityAction betAction, UnityAction passAction)
    {
        betButton.onClick.RemoveAllListeners();
        passButton.onClick.RemoveAllListeners();
        betButton.onClick.AddListener(betAction);
        passButton.onClick.AddListener(passAction);
    }
}
