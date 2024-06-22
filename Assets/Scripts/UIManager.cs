using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
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

    public void UpdatePlayerCardUI(int playerID, Card card)
    {
        if (playerID < playerCards.Length && playerCards[playerID] != null)
        {
            if (card.cardSprite != null)
            {
                playerCards[playerID].sprite = card.cardSprite;
            }
            else
            {
                Debug.LogError("Card sprite is null.");
            }
        }
        else
        {
            Debug.LogError("Invalid playerID or playerCard is null.");
        }
    }

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

    public void DisplayWinner(string winnerMessage)
    {
        if (winnerText != null)
            winnerText.text = winnerMessage;
    }

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
    }
}
