using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players;
    public Dealer dealer;
    public UIManager uiManager;
    private int currentPlayerIndex;
    private int currentBet;

    // ���� ����
    void Start()
    {
        InitializeGame();
    }

    // ���� �ʱ�ȭ
    private void InitializeGame()
    {
        dealer.InitializeDeck();
        currentBet = 0;
        currentPlayerIndex = 0;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Initialize(i);
            Card card = dealer.DealCard();
            players[i].SetCard(card);
            uiManager.UpdatePlayerCardUI(i, card);
            uiManager.UpdateScoreUI(i, players[i].score);
        }
        SetPlayerTurn();
    }

    // �÷��̾� �� ����
    private void SetPlayerTurn()
    {
        uiManager.SetButtonCallbacks(() => Bet(currentPlayerIndex), () => Pass(currentPlayerIndex));
    }

    // ���� ����
    public void Bet(int playerID)
    {
        // ���� ���� ����
        currentBet++; // ���÷� ���� �ݾ��� ������Ŵ
        players[playerID].UpdateScore(currentBet);
        uiManager.UpdateScoreUI(playerID, players[playerID].score);
        NextTurn();
    }

    // �н� ����
    public void Pass(int playerID)
    {
        players[playerID].Pass();
        NextTurn();
    }

    // ���� �� ����
    private void NextTurn()
    {
        do
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        } while (players[currentPlayerIndex].hasPassed);

        SetPlayerTurn();
        CheckGameEnd();
    }

    // ���� ���� üũ
    private void CheckGameEnd()
    {
        int activePlayers = 0;
        Player potentialWinner = null;

        foreach (Player player in players)
        {
            if (!player.hasPassed)
            {
                activePlayers++;
                potentialWinner = player;
            }
        }

        if (activePlayers == 1)
        {
            DeclareWinner(potentialWinner);
        }
    }

    // ���� ����
    private void DeclareWinner(Player winner)
    {
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.score);
        // ���� ���� UI ������Ʈ ���� �߰� ����
    }
}
