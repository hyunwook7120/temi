using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI; // Button과 Image를 사용하기 위해 필요
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요
=======
using UnityEngine.UI;
using TMPro;
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public Image[] playerCards;
    public TextMeshProUGUI[] playerAllChips;
    public TextMeshProUGUI[] playerBettingChips;
    public TextMeshProUGUI winnerText;
    public Button betButton;
    public Button dieButton;
    public Button callButton;
    public Button betPlusOneButton;
    public Button betDoubleButton;
    public Button allInButton;

    // 게임 시작 시 UI 초기화
    public void InitializeGameUI(UnityAction betAction, UnityAction dieAction, UnityAction callAction, UnityAction betPlusOneAction, UnityAction betDoubleAction, UnityAction allInAction)
    {
        SetButtonsInteractable(true);
        if (winnerText != null)
            winnerText.text = "";

        // 버튼 클릭 이벤트 설정
        if (betButton != null) betButton.onClick.RemoveAllListeners();
        if (dieButton != null) dieButton.onClick.RemoveAllListeners();
        if (callButton != null) callButton.onClick.RemoveAllListeners();
        if (betPlusOneButton != null) betPlusOneButton.onClick.RemoveAllListeners();
        if (betDoubleButton != null) betDoubleButton.onClick.RemoveAllListeners();
        if (allInButton != null) allInButton.onClick.RemoveAllListeners();

        if (betButton != null) betButton.onClick.AddListener(betAction);
        if (dieButton != null) dieButton.onClick.AddListener(dieAction);
        if (callButton != null) callButton.onClick.AddListener(callAction);
        if (betPlusOneButton != null) betPlusOneButton.onClick.AddListener(betPlusOneAction);
        if (betDoubleButton != null) betDoubleButton.onClick.AddListener(betDoubleAction);
        if (allInButton != null) allInButton.onClick.AddListener(allInAction);

        // 초기 상태: 베팅 버튼은 보이지 않음, 액션 버튼만 보임
        ToggleBetButtons(false);
        ToggleActionButtons(true);
    }

>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    public void UpdatePlayerCardUI(int playerID, Card card)
    {
        if (playerID < playerCards.Length && playerCards[playerID] != null)
        {
<<<<<<< HEAD
            SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                playerCards[playerID].sprite = spriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("Card does not have a SpriteRenderer component.");
=======
            if (card.cardSprite != null)
            {
                playerCards[playerID].sprite = card.cardSprite;
            }
            else
            {
                Debug.LogError("Card sprite is null.");
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
            }
        }
        else
        {
            Debug.LogError("Invalid playerID or playerCard is null.");
        }
    }

<<<<<<< HEAD
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
=======
    public void UpdatePlayerAllChipsUI(int playerID, int chips)
    {
        if (playerID < playerAllChips.Length && playerAllChips[playerID] != null)
        {
            playerAllChips[playerID].text = "All Chips: " + chips;
        }
        else
        {
            Debug.LogError("Invalid playerID or playerAllChips is null.");
        }
    }

    public void UpdatePlayerBettingChipsUI(int playerID, int chips)
    {
        if (playerID < playerBettingChips.Length && playerBettingChips[playerID] != null)
        {
            playerBettingChips[playerID].text = "Betting Chips: " + chips;
        }
        else
        {
            Debug.LogError("Invalid playerID or playerBettingChips is null.");
        }
    }

>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    public void DisplayWinner(string winnerMessage)
    {
        if (winnerText != null)
            winnerText.text = winnerMessage;
    }
<<<<<<< HEAD
    
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
=======

    public void SetButtonsInteractable(bool interactable)
    {
        if (betButton != null) betButton.interactable = interactable;
        if (dieButton != null) dieButton.interactable = interactable;
        if (callButton != null) callButton.interactable = interactable;
        if (betPlusOneButton != null) betPlusOneButton.interactable = interactable;
        if (betDoubleButton != null) betDoubleButton.interactable = interactable;
        if (allInButton != null) allInButton.interactable = interactable;
    }

    public void ToggleBetButtons(bool show)
    {
        betPlusOneButton.gameObject.SetActive(show);
        betDoubleButton.gameObject.SetActive(show);
        allInButton.gameObject.SetActive(show);
    }

    public void ToggleActionButtons(bool show)
    {
        betButton.gameObject.SetActive(show);
        dieButton.gameObject.SetActive(show);
        callButton.gameObject.SetActive(show);
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    }
}
