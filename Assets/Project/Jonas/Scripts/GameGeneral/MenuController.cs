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

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressTxt;
    public List<Texture> rawTexture;
    public RawImage rawImage;
    public Text tipsText;


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
        StartCoroutine(LoadAsyncScene());
    }


    IEnumerator LoadAsyncScene()
    {

        AsyncOperation operation = PhotonNetwork.LoadLevelAsync("Map");

        loadingScreen.SetActive(true);
        StartCoroutine("ChangeTexture");
        StartCoroutine("ChangeTips");

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressTxt.text = progress * 100f + "%";

            yield return null;
        }

    }

    IEnumerator ChangeTexture()
    {
        rawImage.texture = rawTexture[0];

        yield return new WaitForSeconds(5f);

        rawImage.texture = rawTexture[1];

        yield return new WaitForSeconds(5f);

        rawImage.texture = rawTexture[2];

        yield return new WaitForSeconds(5f);

        StartCoroutine("ChangeTexture");
    }

    IEnumerator ChangeTips()
    {
        tipsText.text = "Beware of golems, they have special attacks when reaching a certain amount of life.";

        yield return new WaitForSeconds(7f);

        tipsText.text = "Watch out for witches they just look weak.";

        yield return new WaitForSeconds(7f);

        tipsText.text = "You can buy equipment and potions by talking to the bald man in the middle of the village of aurora.";

        yield return new WaitForSeconds(7f);

        tipsText.text = "Each enemy has up to two attack variations, except the boss ...";

        yield return new WaitForSeconds(7f);

        tipsText.text = "Make your story, tell your adventures, around the bonfire, this is the purpose of the game.";

        yield return new WaitForSeconds(7f);

        StartCoroutine("ChangeTips");

    }
}
