using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players;
    public Dealer dealer;
    public UIManager uiManager;
    private int currentPlayerIndex;
    private int currentBet;

    // 게임 시작
    void Start()
    {
        InitializeGame();
    }

    // 게임 초기화
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

    // 플레이어 턴 설정
    private void SetPlayerTurn()
    {
        uiManager.SetButtonCallbacks(() => Bet(currentPlayerIndex), () => Pass(currentPlayerIndex));
    }

    // 베팅 로직
    public void Bet(int playerID)
    {
        // 베팅 로직 구현
        currentBet++; // 예시로 베팅 금액을 증가시킴
        players[playerID].UpdateScore(currentBet);
        uiManager.UpdateScoreUI(playerID, players[playerID].score);
        NextTurn();
    }

    // 패스 로직
    public void Pass(int playerID)
    {
        players[playerID].Pass();
        NextTurn();
    }

    // 다음 턴 진행
    private void NextTurn()
    {
        do
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        } while (players[currentPlayerIndex].hasPassed);

        SetPlayerTurn();
        CheckGameEnd();
    }

    // 게임 종료 체크
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

    // 승자 결정
    private void DeclareWinner(Player winner)
    {
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.score);
        // 게임 종료 UI 업데이트 로직 추가 가능
    }
}