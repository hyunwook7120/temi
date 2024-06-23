using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreatingRoom(string roomName)
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomName);
            Debug.Log("Creating Room..");
        }
        else
        {
            Debug.LogError("Not connected to Photon Network");
        }
    }

    public void JoiningRoom(string roomName)
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(roomName);
            Debug.Log("Joining Room..");
        }
        else
        {
            Debug.LogError("Not connected to Photon Network");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.LogError(message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message){
        Debug.LogError(message);
    }
    
    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("TestScene");
        Debug.Log("Create Success");
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("TestScene");
        Debug.Log("Join Success");
    }
}
