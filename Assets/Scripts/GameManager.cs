using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
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
=======
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
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        }
        InitializeGame();
    }

    // 게임 초기화
    private void InitializeGame()
    {
        dealer.InitializeDeck();
<<<<<<< HEAD
        dealer.ShuffleDeck();
        currentBet = 0;
        currentPlayerIndex = 0;
        isGameActive = true;  // 게임을 활성화 상태로 설정
=======
        dealer.ShuffleDeck(); // 덱을 초기화할 때 셔플
        currentBet = 1; // 기본으로 칩 한 개를 걸고 시작
        currentPlayerIndex = 0;
        isGameActive = true;
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Initialize(i);
            Card card = dealer.DealCard();
            players[i].SetCard(card);
            uiManager.UpdatePlayerCardUI(i, card);
<<<<<<< HEAD
            uiManager.UpdateScoreUI(i, players[i].score);
=======
            uiManager.UpdatePlayerAllChipsUI(i, players[i].allChips);
            uiManager.UpdatePlayerBettingChipsUI(i, players[i].bettingChips);
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        }
        SetPlayerTurn();
    }

    // 플레이어 턴 설정
    private void SetPlayerTurn()
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        foreach (Player player in players)
        {
            if (!player.hasPassed)
            {
                activePlayers++;
            }
        }

<<<<<<< HEAD
        if (activePlayers == 0)  // 모든 플레이어가 패스했을 경우 게임 종료
=======
        if (activePlayers == 0)
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        {
            Player winner = FindWinner();
            if (winner != null)
            {
                DeclareWinner(winner);
            }
        }
        else
        {
<<<<<<< HEAD
=======
            // 기본 베팅 칩 초기화
            currentBet = 1;
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
            SetPlayerTurn();
        }
    }

    // 게임 종료 체크
    private void CheckGameEnd()
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        Player winner = null;
        int highestScore = 0;
        foreach (Player player in players)
        {
<<<<<<< HEAD
            if (!player.hasPassed && player.score > highestScore)
            {
                winner = player;
                highestScore = player.score;
=======
            if (!player.hasPassed && player.bettingChips > highestScore)
            {
                winner = player;
                highestScore = player.bettingChips;
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
            }
        }
        return winner;
    }
<<<<<<< HEAD
    
    // 승자 결정
    private void DeclareWinner(Player winner)
    {
        isGameActive = false;
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.score);
=======

    // 승자 선언
    private void DeclareWinner(Player winner)
    {
        isGameActive = false;
        Debug.Log("The winner is Player " + winner.playerID + " with a score of " + winner.bettingChips);
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
        UpdateUIForWinner(winner);
        DisableGameplayElements();
        EndGame();
    }

<<<<<<< HEAD
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

=======
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
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
    private void EndGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 9fe68ebbed408a3a0a0815758d9b01dbd2bf961f
