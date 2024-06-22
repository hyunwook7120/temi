using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Player[] players; // 플레이어 배열
    public Dealer dealer; // 딜러
    public UIManager uiManager; // UI 관리자
    private int currentPlayerIndex; // 현재 플레이어 인덱스
    private int currentBet; // 현재 베팅
    private bool isGameActive; // 게임 활성 상태
    private int betPlusOneCount = 0; // BetPlusOne 횟수 카운트
    private const int maxBetPlusOneCount = 2; // BetPlusOne 최대 횟수

    void Start()
    {
        // CardManager가 초기화되었는지 확인
        if (CardManager.Instance == null)
        {
            Debug.LogError("CardManager is not initialized.");
            return;
        }
        InitializeGame();
    }

    // 게임 초기화
    private void InitializeGame()
    {
        dealer.InitializeDeck();
        dealer.ShuffleDeck(); // 덱을 초기화할 때 셔플
        currentBet = 1; // 기본으로 칩 한 개를 걸고 시작
        currentPlayerIndex = 0;
        isGameActive = true;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Initialize(i);
            Card card = dealer.DealCard();
            players[i].SetCard(card);
            uiManager.UpdatePlayerCardUI(i, card);
            uiManager.UpdatePlayerAllChipsUI(i, players[i].allChips);
            uiManager.UpdatePlayerBettingChipsUI(i, players[i].bettingChips);
        }
        SetPlayerTurn();
    }

    // 플레이어 턴 설정
    private void SetPlayerTurn()
    {
        uiManager.InitializeGameUI(
            () => Bet(currentPlayerIndex), 
            () => Die(currentPlayerIndex), 
            () => Call(currentPlayerIndex), 
            () => BetPlusOne(currentPlayerIndex), 
            () => BetDouble(currentPlayerIndex), 
            () => AllIn(currentPlayerIndex)
        );
    }

    // 베팅
    public void Bet(int playerID)
    {
        if (!isGameActive || playerID < 0 || playerID >= players.Length) return;
        currentBet = 1;
        players[playerID].bettingChips = currentBet;
        uiManager.UpdatePlayerBettingChipsUI(playerID, players[playerID].bettingChips);
        // 베팅 버튼 토글
        uiManager.ToggleBetButtons(true);
        uiManager.ToggleActionButtons(false);
    }

    // 콜
    public void Call(int playerID)
    {
        if (!isGameActive || playerID < 0 || playerID >= players.Length) return;
        DetermineRoundWinner();
        NextTurn();
    }

    // 패스
    public void Die(int playerID)
    {
        if (!isGameActive || playerID < 0 || playerID >= players.Length) return;
        players[playerID].Pass();
        foreach (Player player in players)
        {
            Card newCard = dealer.DealCard();
            if (newCard != null)
            {
                player.SetCard(newCard);
                uiManager.UpdatePlayerCardUI(player.playerID, newCard);
            }
            else
            {
                Debug.LogError("No more cards to deal.");
            }
        }
        DetermineRoundWinner();
        NextTurn();
    }

    // 베팅 +1
    public void BetPlusOne(int playerID)
    {
        if (!isGameActive || betPlusOneCount >= maxBetPlusOneCount || playerID < 0 || playerID >= players.Length) return;
        currentBet++;
        betPlusOneCount++;
        players[playerID].bettingChips = currentBet;
        players[playerID].allChips -= 1;
        uiManager.UpdatePlayerAllChipsUI(playerID, players[playerID].allChips);
        uiManager.UpdatePlayerBettingChipsUI(playerID, players[playerID].bettingChips);
        CheckGameEnd();
        if (isGameActive)
        {
            // 기본 액션 버튼으로 돌아가기
            uiManager.ToggleBetButtons(false);
            uiManager.ToggleActionButtons(true);
        }
    }

    // 베팅 더블
    public void BetDouble(int playerID)
    {
        if (!isGameActive || playerID < 0 || playerID >= players.Length || players[playerID].allChips < currentBet * 2) return;
        currentBet *= 2;
        players[playerID].bettingChips = currentBet;
        players[playerID].allChips -= currentBet / 2; // 이전 베팅값을 빼줌
        uiManager.UpdatePlayerAllChipsUI(playerID, players[playerID].allChips);
        uiManager.UpdatePlayerBettingChipsUI(playerID, players[playerID].bettingChips);
        CheckGameEnd();
        if (isGameActive)
        {
            // 기본 액션 버튼으로 돌아가기
            uiManager.ToggleBetButtons(false);
            uiManager.ToggleActionButtons(true);
        }
    }

    // 올인
    public void AllIn(int playerID)
    {
        if (!isGameActive || playerID < 0 || playerID >= players.Length) return;
        currentBet = players[playerID].allChips;
        players[playerID].bettingChips = currentBet;
        players[playerID].allChips = 0;
        uiManager.UpdatePlayerAllChipsUI(playerID, players[playerID].allChips);
        uiManager.UpdatePlayerBettingChipsUI(playerID, players[playerID].bettingChips);
        CheckGameEnd();
        if (isGameActive)
        {
            // 기본 액션 버튼으로 돌아가기
            uiManager.ToggleBetButtons(false);
            uiManager.ToggleActionButtons(true);
        }
    }

    // 라운드 승자 결정
    private void DetermineRoundWinner()
    {
        Player player1 = players[0];
        Player player2 = players[1];

        if (player1.card.value > player2.card.value)
        {
            player1.allChips += player1.bettingChips + player2.bettingChips;
        }
        else if (player2.card.value > player1.card.value)
        {
            player2.allChips += player1.bettingChips + player2.bettingChips;
        }

        player1.bettingChips = 0;
        player2.bettingChips = 0;

        uiManager.UpdatePlayerAllChipsUI(0, player1.allChips);
        uiManager.UpdatePlayerAllChipsUI(1, player2.allChips);
        uiManager.UpdatePlayerBettingChipsUI(0, player1.bettingChips);
        uiManager.UpdatePlayerBettingChipsUI(1, player2.bettingChips);
    }

    // 다음 턴 진행
    private void NextTurn()
    {
        dealer.ShuffleDeck(); // 다음 턴 시작 시 덱 셔플

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        int activePlayers = 0;
        foreach (Player player in players)
        {
            if (!player.hasPassed)
            {
                activePlayers++;
            }
        }

        if (activePlayers == 0)
        {
            Player winner = FindWinner();
            if (winner != null)
            {
                DeclareWinner(winner);
            }
        }
        else
        {
            // 기본 베팅 칩 초기화
            currentBet = 1;
            SetPlayerTurn();
        }
    }

    // 게임 종료 체크
    private void CheckGameEnd()
    {
        foreach (Player player in players)
        {
            if (player.allChips <= 0)
            {
                DeclareWinner(player);
                break;
            }
        }
    }

    // 승자 찾기
    private Player FindWinner()
    {
        Player winner = null;
        int highestScore = 0;
        foreach (Player player in players)
        {
            if (!player.hasPassed && player.bettingChips > highestScore)
            {
                winner = player;
                highestScore = player.bettingChips;
            }
        }
        return winner;
    }

    // 승자 선언
    private void DeclareWinner(Player winner)
    {
        isGameActive = false;
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.bettingChips);
        UpdateUIForWinner(winner);
        DisableGameplayElements();
        EndGame();
    }

    // 승자 UI 업데이트
    private void UpdateUIForWinner(Player winner)
    {
        uiManager.DisplayWinner($"Winner: Player {winner.playerID} Score: {winner.bettingChips}");
    }

    // 게임 플레이 관련 UI 요소 비활성화
    private void DisableGameplayElements()
    {
        uiManager.SetButtonsInteractable(false);
    }

    // 게임 종료
    private void EndGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
