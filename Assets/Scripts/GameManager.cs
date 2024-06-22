using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Player[] players;
    public Dealer dealer;
    public UIManager uiManager;
    private int currentPlayerIndex;
    private int currentBet;
    private bool isGameActive;
    
    // 게임 시작
    void Start()
    {
         if (CardManager.Instance == null)
        {
            Debug.LogError("CardManager is not initialized.");
            return; // CardManager가 준비되지 않았으면 초기화 중단
        }
        InitializeGame();
    }

    // 게임 초기화
    private void InitializeGame()
    {
        dealer.InitializeDeck();
        dealer.ShuffleDeck();
        currentBet = 0;
        currentPlayerIndex = 0;
        isGameActive = true;  // 게임을 활성화 상태로 설정
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
        if (!isGameActive) return;
        currentBet++;
        // 베팅한 플레이어의 점수만 변경하는 대신 모든 플레이어의 점수를 업데이트
        players[playerID].UpdateScore(currentBet); // 베팅한 플레이어의 점수만 증가
        uiManager.UpdateScoreUI(playerID, players[playerID].score);
        NextTurn();
    }

    // 패스 로직
    public void Pass(int playerID)
    {
        if (!isGameActive) return;
        players[playerID].Pass();
        Card newCard = dealer.DealCard();
        if (newCard != null)
        {
            players[playerID].SetCard(newCard);
            uiManager.UpdatePlayerCardUI(playerID, newCard);
        }   
        else
        {
            Debug.LogError("No more cards to deal.");
        }
        NextTurn();
    }

    // 다음 턴 진행
    private void NextTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        int activePlayers = 0;  // 변수 선언과 초기화
        foreach (Player player in players)
        {
            if (!player.hasPassed)
            {
                activePlayers++;
            }
        }

        if (activePlayers == 0)  // 모든 플레이어가 패스했을 경우 게임 종료
        {
            Player winner = FindWinner();
            if (winner != null)
            {
                DeclareWinner(winner);
            }
        }
        else
        {
            SetPlayerTurn();
        }
    }

    // 게임 종료 체크
    private void CheckGameEnd()
    {
         int activePlayers = 0; // 변수 선언과 초기화
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

    private Player FindWinner()
    {
        // 가장 높은 점수를 가진 플레이어를 승자로 결정
        Player winner = null;
        int highestScore = 0;
        foreach (Player player in players)
        {
            if (!player.hasPassed && player.score > highestScore)
            {
                winner = player;
                highestScore = player.score;
            }
        }
        return winner;
    }
    
    // 승자 결정
    private void DeclareWinner(Player winner)
    {
        isGameActive = false;
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.score);
        UpdateUIForWinner(winner);
        DisableGameplayElements();
        EndGame();
    }

    private void UpdateUIForWinner(Player winner)
    {
        // 승자 정보 업데이트
        uiManager.DisplayWinner($"Winner: Player {winner.playerID} Score: {winner.score}");
    }

    private void DisableGameplayElements()
    {
        // 게임 플레이 관련 UI 요소 비활성화
        uiManager.SetButtonsInteractable(false);
    }

    private void EndGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}