using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyButton : MonoBehaviour
{
    public LobbyManager lm;
    public TMP_InputField RoomNameInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(int num){
        if(num == 1) lm.CreatingRoom(RoomNameInput.text);
        else lm.JoiningRoom(RoomNameInput.text);
    }
}
