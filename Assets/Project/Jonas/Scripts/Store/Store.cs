using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public GameObject store;

    private string npcTag;
    private bool canAcessStore = false;
    private bool canExecuteVoice = false;
    public Font font;
    public AudioSource audioSource;

    private void Update()
    {

        if (store.active && Input.GetKeyDown(KeyCode.Escape))
        {
            closeStore();
            canExecuteVoice = true;
        }

        if (canAcessStore && Input.GetKeyDown(KeyCode.F))
        {

            if (audioSource != null && canExecuteVoice)
            {
                audioSource.Play();
                canExecuteVoice = false;
            }

            openStore();

            
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        canAcessStore = true;
        npcTag = other.tag;

        if(other.tag == "npcStore")
        {
            audioSource = other.GetComponent<AudioSource>();
            canExecuteVoice = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        canAcessStore = false;
        npcTag = null;
        canExecuteVoice = false;

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
