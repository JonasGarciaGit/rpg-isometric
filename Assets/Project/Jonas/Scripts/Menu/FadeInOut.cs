using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    public Image blackFade;
    private bool canStartGame;

    // Start is called before the first frame update
    void Start()
    {
        canStartGame = false;
        blackFade.canvasRenderer.SetAlpha(1f);
        FadeOutImage();
        StartCoroutine("canFadeIn");
    }

    private void Update()
    {
        StartGame();
    }

    public void FadeOutImage()
    {
        blackFade.CrossFadeAlpha(0.0f, 9f, false);
    }

    public void FadeInImage()
    {
        blackFade.CrossFadeAlpha(1f, 5f, false);
    }


    IEnumerator canFadeIn()
    {
        yield return new WaitForSeconds(4f);
        FadeInImage();
        yield return new WaitForSeconds(4f);
        canStartGame = true;
    }

    private void StartGame()
    {
        if (canStartGame)
        {
            Application.LoadLevel("Menu");
        }
    }
}
