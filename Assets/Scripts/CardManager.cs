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
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    // 카드 프리팹을 찾는 메서드
    public GameObject GetCardPrefab(int value)
    {
        if (value >= 1 && value <= cardPrefabs.Length)
        {
            return cardPrefabs[value - 1];
        }
        return null;
    }
}