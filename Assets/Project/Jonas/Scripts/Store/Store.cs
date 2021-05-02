using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public GameObject store;

    private bool canAcessStore = false;

    private void Update()
    {
        if (store.active && Input.GetKeyDown(KeyCode.Escape))
        {
            closeStore();
        }

        if(canAcessStore && Input.GetKeyDown(KeyCode.F))
        {
            openStore();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        canAcessStore = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canAcessStore = false;
    }

    public void openStore()
    {
        store.SetActive(true);
    }

    public void closeStore()
    {
        store.SetActive(false);
    }

}
