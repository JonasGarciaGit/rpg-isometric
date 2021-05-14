using UnityEngine;

public class InteractCharacter : Interactable
{

    public string characterName;
    public GameObject[] characters;
    public bool hasCharacterSelected = false;
    public GameObject menu;

    public override void Interact()
    {
        base.Interact();

        SelectedCharacter();
    }

    void SelectedCharacter()
    {
        if (gameObject.GetComponent<SelectCharacterOutline>().canSelect) {
            Debug.Log("Selecting " + characterName);
            PlayerPrefs.SetString("character", characterName);

            characters[0].GetComponent<Outline>().enabled = true;
            characters[1].GetComponent<Outline>().enabled = false;
            characters[2].GetComponent<Outline>().enabled = false;
            characters[3].GetComponent<Outline>().enabled = false;

            hasCharacterSelected = true;
            menu.GetComponent<MenuButtonController>().hasCharacterSelected = hasCharacterSelected;
        }
    }

}
