using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{


    public Camera camera;
    private bool canZoomOut;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Timer");
    }

    private void Update()
    {
        if (canZoomOut)
        {
            zoomOut();
        }
       
    }

    private void zoomOut()
    {
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 90f, Time.deltaTime * 0.2f);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        canZoomOut = true;
    }


}
