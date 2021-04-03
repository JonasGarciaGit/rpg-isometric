using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;

    private void Awake()
    {
        GameCanvas.SetActive(true);
    }

    public void SpanwnPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(0, 0), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
}
