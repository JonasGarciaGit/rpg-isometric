using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject ConnectPanel;

    public Text nickname;
    public Text createLobbyCode;
    public Text joinLobbyCode;

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
        PhotonNetwork.playerName = nickname.text;
        PhotonNetwork.player.NickName = nickname.text;
    }

    public void StartGame()
    {
        if (gameObject.GetComponent<MenuButtonController>().mode.Equals("createGame"))
        {
            CreateGame();
        }

        if (gameObject.GetComponent<MenuButtonController>().mode.Equals("joinGame"))
        {
            JoinGame();
        }
    }

    public void CreateGame()
    {
        SetUserName();
        PhotonNetwork.CreateRoom(createLobbyCode.text, new RoomOptions { maxPlayers = 4 }, null);
    }

    public void JoinGame()
    {
        SetUserName();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(joinLobbyCode.text, roomOptions, TypedLobby.Default);
    }


    private void OnJoinedRoom()
    {
            PhotonNetwork.LoadLevel("Map");
    }
}
