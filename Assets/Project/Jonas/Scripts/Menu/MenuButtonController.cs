using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{

    public string mode;

    //buttons
    public GameObject createButton;
    public GameObject joinButton;
    public GameObject quitButton;
    public GameObject startButton;
    public GameObject backButton;
    public GameObject nicknameInput;
    public GameObject selectTitle;
    public Text nickname;
    public Text createLobbyCodeText;
    public Text joinLobbyCodeText;
    public GameObject createLobbyCodeInput;
    public GameObject joinLobbyCodeInput;

    //characters
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;

    public bool hasCharacterSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
        nicknameInput.SetActive(false);
        selectTitle.SetActive(false);
        backButton.SetActive(false);
        createLobbyCodeInput.SetActive(false);
        joinLobbyCodeInput.SetActive(false);
        nicknameInput.GetComponent<InputField>().characterLimit = 15;
        createLobbyCodeInput.GetComponent<InputField>().characterLimit = 6;
        joinLobbyCodeInput.GetComponent<InputField>().characterLimit = 6;
    }

    private void Update()
    {
        if (nickname.text.Length > 0 && hasCharacterSelected  && (createLobbyCodeText.text.Length > 0 || joinLobbyCodeText.text.Length > 0))
        {
            startButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            startButton.GetComponent<Button>().interactable = false;
        }
    }


    public void CreateGame()
    {
        mode = "createGame";

        createButton.SetActive(false);
        joinButton.SetActive(false);
        quitButton.SetActive(false);

        startButton.SetActive(true);
        nicknameInput.SetActive(true);
        selectTitle.SetActive(true);
        backButton.SetActive(true);
        createLobbyCodeInput.SetActive(true);

        character1.GetComponent<SelectCharacterOutline>().canSelect = true;
        character2.GetComponent<SelectCharacterOutline>().canSelect = true;
        character3.GetComponent<SelectCharacterOutline>().canSelect = true;
        character4.GetComponent<SelectCharacterOutline>().canSelect = true;
    }

    public void JoinGame()
    {
        mode = "joinGame";

        createButton.SetActive(false);
        joinButton.SetActive(false);
        quitButton.SetActive(false);

        startButton.SetActive(true);
        nicknameInput.SetActive(true);
        selectTitle.SetActive(true);
        backButton.SetActive(true);
        joinLobbyCodeInput.SetActive(true);

        character1.GetComponent<SelectCharacterOutline>().canSelect = true;
        character2.GetComponent<SelectCharacterOutline>().canSelect = true;
        character3.GetComponent<SelectCharacterOutline>().canSelect = true;
        character4.GetComponent<SelectCharacterOutline>().canSelect = true;
    }

    public void BackGame()
    {
        mode = "";

        createButton.SetActive(true);
        joinButton.SetActive(true);
        quitButton.SetActive(true);

        startButton.SetActive(false);
        nicknameInput.SetActive(false);
        selectTitle.SetActive(false);
        backButton.SetActive(false);
        createLobbyCodeInput.SetActive(false);
        joinLobbyCodeInput.SetActive(false);

        character1.GetComponent<SelectCharacterOutline>().canSelect = false;
        character2.GetComponent<SelectCharacterOutline>().canSelect = false;
        character3.GetComponent<SelectCharacterOutline>().canSelect = false;
        character4.GetComponent<SelectCharacterOutline>().canSelect = false;

        hasCharacterSelected = false;
        joinLobbyCodeText.text = "";
        createLobbyCodeText.text = "";

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
