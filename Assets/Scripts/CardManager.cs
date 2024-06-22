using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public GameObject[] cardPrefabs; // 모든 카드 프리팹을 배열로 저장 (1부터 10까지)

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
<<<<<<< HEAD
            Debug.Log("CardManager instance created.");
        }
        else
        {
            Debug.LogError("Duplicate CardManager instance detected!");
=======
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
>>>>>>> 3ca0060983a34c3b490d493cab5031f5e75f70a8
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    // 카드 프리팹을 찾는 메서드
    public GameObject GetCardPrefab(int value)
    {
    foreach (GameObject cardPrefab in cardPrefabs)
    {
        Card card = cardPrefab.GetComponent<Card>();
        if (card != null && card.value == value)
        {
            return cardPrefab;
        }
    }
    return null;
    }

    // 덱 초기화
    public void InitializeDeck()
    {
        // 카드 덱 초기화 로직
    }

    // 카드를 섞는 메서드
    public void ShuffleDeck()
    {
        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            GameObject temp = cardPrefabs[i];
            int randomIndex = Random.Range(0, cardPrefabs.Length);
            cardPrefabs[i] = cardPrefabs[randomIndex];
            cardPrefabs[randomIndex] = temp;
        }
    }

    // 카드를 나누는 메서드
    public GameObject DealCard()
    {
        foreach (GameObject cardPrefab in cardPrefabs)
        {
            if (!cardPrefab.activeInHierarchy)
            {
                cardPrefab.SetActive(true);
                return cardPrefab;
            }
        }
        return null;
    }
}