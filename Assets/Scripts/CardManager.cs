using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public GameObject[] cardPrefabs; // ��� ī�� �������� �迭�� ���� (1���� 10����)

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� �ı�
        }
    }

    // ī�� �������� ã�� �޼���
    public GameObject GetCardPrefab(int value)
    {
        if (value >= 1 && value <= cardPrefabs.Length)
        {
            return cardPrefabs[value - 1];
        }
        return null;
    }
}
