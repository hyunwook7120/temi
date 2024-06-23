using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
    }
}
