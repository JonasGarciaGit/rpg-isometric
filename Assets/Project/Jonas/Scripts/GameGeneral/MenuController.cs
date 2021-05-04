using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject ConnectPanel;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);

    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("connected");
    }


    public void SetUserName()
    {
        int randomValue = Random.Range(1, 20);
        PhotonNetwork.playerName = "Player" + randomValue;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom("server1", new RoomOptions { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom("server1", roomOptions, TypedLobby.Default);
    }


    private void OnJoinedRoom()
    {
            PhotonNetwork.LoadLevel("Map");
    }
}
