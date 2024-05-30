using UnityEngine;
using UnityEngine.UI; // Button�� Image�� ����ϱ� ���� �ʿ�
using TMPro; // TextMeshProUGUI�� ����ϱ� ���� �ʿ�
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Image[] playerCards; // �� �÷��̾��� ī�� Image �迭
    public TextMeshProUGUI[] playerScores; // �� �÷��̾��� ���� TextMeshProUGUI �迭
    public Button betButton;
    public Button passButton;

    // �÷��̾� ī�� UI ������Ʈ
    public void UpdatePlayerCardUI(int playerID, Card card)
    {
        if (playerID < playerCards.Length && playerCards[playerID] != null)
        {
            playerCards[playerID].sprite = card.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // ���� UI ������Ʈ
    public void UpdateScoreUI(int playerID, int score)
    {
        if (playerID < playerScores.Length && playerScores[playerID] != null)
        {
            playerScores[playerID].text = "Score: " + score;
        }
    }

    // ���� �� �н� ��ư ����
    public void SetButtonCallbacks(UnityAction betAction, UnityAction passAction)
    {
        betButton.onClick.RemoveAllListeners();
        passButton.onClick.RemoveAllListeners();
        betButton.onClick.AddListener(betAction);
        passButton.onClick.AddListener(passAction);
    }
}
