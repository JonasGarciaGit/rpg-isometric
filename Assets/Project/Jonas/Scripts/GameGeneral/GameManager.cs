using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject SceneCamera;


    private void Awake()
    {
        SpanwnPlayer();
    }

    public void SpanwnPlayer()
    {
            PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            Debug.Log("Instanciei um player");
            SceneCamera.SetActive(false);                
    }
}
