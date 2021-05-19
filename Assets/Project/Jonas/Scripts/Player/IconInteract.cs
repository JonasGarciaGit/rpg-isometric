using UnityEngine;

public class IconInteract : MonoBehaviour
{
    public GameObject window;

    public void OpenOrCloseWindow()
    {
        window.SetActive(!window.activeSelf);
    }

}
