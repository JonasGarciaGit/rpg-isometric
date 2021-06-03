using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefabKnight;
    public GameObject PlayerPrefabWarrior;
    public GameObject PlayerPrefabViking;
    public GameObject PlayerPrefabThief;
    public GameObject crow;
    public GameObject SceneCamera;


    private void Awake()
    {
        SpanwnPlayer();
    }

    public void SpanwnPlayer()
    {
        if (PlayerPrefs.GetString("character").Equals("Knight"))
        {
            PhotonNetwork.Instantiate(PlayerPrefabKnight.name, new Vector3(70f, 0, -68f), Quaternion.identity, 0);
            Debug.Log("Instanciei um player");
            SceneCamera.SetActive(false);
        }

        if (PlayerPrefs.GetString("character").Equals("Viking"))
        {
            PhotonNetwork.Instantiate(PlayerPrefabViking.name, new Vector3(70f, 0, -68f), Quaternion.identity, 0);
            Debug.Log("Instanciei um player");
            SceneCamera.SetActive(false);
        }

        if (PlayerPrefs.GetString("character").Equals("Warrior"))
        {
            PhotonNetwork.Instantiate(PlayerPrefabWarrior.name, new Vector3(70f, 0, -68f), Quaternion.identity, 0);
            Debug.Log("Instanciei um player");
            SceneCamera.SetActive(false);
        }

        if (PlayerPrefs.GetString("character").Equals("Thief"))
        {
            PhotonNetwork.Instantiate(PlayerPrefabThief.name, new Vector3(70f, 0, -68f), Quaternion.identity, 0);
            Debug.Log("Instanciei um player");
            SceneCamera.SetActive(false);
        }


    }
}
