using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{

    public GameObject menuInGame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menuInGame.SetActive(!menuInGame.activeSelf);
        }
    }

    public void mainMenu()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel("Menu");
    }

    public void QuitGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
        Application.Quit();
    }

}
