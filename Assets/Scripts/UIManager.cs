using UnityEngine;
using UnityEngine.UI; // Button과 Image를 사용하기 위해 필요
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Image[] playerCards; // 각 플레이어의 카드 Image 배열
    public TextMeshProUGUI[] playerScores; // 각 플레이어의 점수 TextMeshProUGUI 배열
    public TextMeshProUGUI winnerText; // 승자를 표시하는 텍스트 필드
    public Button betButton;
    public Button passButton;

     // 게임 시작 시 UI 초기화
    public void InitializeGameUI()
    {
        SetButtonsInteractable(true);
        if (winnerText != null)
            winnerText.text = ""; // 승자 텍스트 초기화
    }

    // 플레이어 카드 UI 업데이트
    public void UpdatePlayerCardUI(int playerID, Card card)
    {
        if (playerID < playerCards.Length && playerCards[playerID] != null)
        {
            SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                playerCards[playerID].sprite = spriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("Card does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Invalid playerID or playerCard is null.");
        }
    }

    // 점수 UI 업데이트
    public void UpdateScoreUI(int playerID, int score)
    {
        if (playerID < playerScores.Length && playerScores[playerID] != null)
        {
            playerScores[playerID].text = "Score: " + score;
        }
        else
        {
            Debug.LogError("Invalid playerID or playerScore is null.");
        }
    }
    // 승자 표시
    public void DisplayWinner(string winnerMessage)
    {
        if (winnerText != null)
            winnerText.text = winnerMessage;
    }
    
    // 베팅 및 패스 버튼 설정
    public void SetButtonCallbacks(UnityAction betAction, UnityAction passAction)
    {
        betButton.onClick.RemoveAllListeners();
        passButton.onClick.RemoveAllListeners();
        betButton.onClick.AddListener(betAction);
        passButton.onClick.AddListener(passAction);
    }

    // 버튼 활성화/비활성화 설정
    public void SetButtonsInteractable(bool interactable)
    {
        betButton.interactable = interactable;
        passButton.interactable = interactable;
    }
}
