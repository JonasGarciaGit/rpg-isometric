using UnityEngine;

public class HideObjects : MonoBehaviour
{

    GameObject currentGameObject;
    GameObject actualObj;
    public Camera camera;
    public Renderer tempRend;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        try
        {

            RaycastHit hit;
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));


            if (Physics.Raycast(ray, out hit, 100))
            {
                
                Renderer rend = hit.collider.transform.GetComponent<Renderer>();


                if (rend.tag == "hideObject") //Se 
                {
                    if(tempRend != null)
                    {
                        tempRend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                        tempRend = null;
                    }

                    tempRend = rend;
                    rend.material.shader = Shader.Find("Shader Graphs/Transparency");
                  
                }

                if(rend.tag != "hideObject" && tempRend != null)
                {
                   
                    tempRend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    tempRend = null;
                }

            }
          



            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        catch
        {

        }

    }
}

