using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterOutline : MonoBehaviour
{

    public GameObject character;
    public bool canSelect = false;

    private void OnMouseEnter()
    {
        if (canSelect)
        {
            character.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (!gameObject.GetComponent<InteractCharacter>().hasCharacterSelected)
        {
            character.GetComponent<Outline>().enabled = false;
        }

        gameObject.GetComponent<InteractCharacter>().hasCharacterSelected = false;
    }

}
