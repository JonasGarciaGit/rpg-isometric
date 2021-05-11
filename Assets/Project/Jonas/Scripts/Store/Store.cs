using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public GameObject store;

    private string npcTag;
    private bool canAcessStore = false;

    private void Update()
    {

        if (store.active && Input.GetKeyDown(KeyCode.Escape))
        {
            closeStore();
        }

        if (canAcessStore && Input.GetKeyDown(KeyCode.F))
        {
            openStore();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        canAcessStore = true;
        npcTag = other.tag;
    }

    private void OnTriggerExit(Collider other)
    {
        canAcessStore = false;
        npcTag = null;

    }

    public void openStore()
    {
        if (npcTag == null)
            return;

           if (npcTag.Equals("npcStore"))
            {
                store.SetActive(true);
            }

    }

    public void closeStore()
    {
        store.SetActive(false);
    }

}
