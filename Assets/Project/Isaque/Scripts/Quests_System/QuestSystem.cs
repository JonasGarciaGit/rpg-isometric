using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{

    private bool canAcessQuest = false;

    public GameObject QuestUI;

    public Text questDescriptionTextUI;
    public Text questRewardTextUI;
    public TextMeshProUGUI newQuestInstructionsUI;
    public string basicIntruction;
    public string rewardQuest;

    public List<string> questsLines = new List<string>();

    QuestInformation informations;

    public bool haveQuest = false;

    public bool canCompletedQuest = false;

    public Text coinInInventory;

    public TextMeshProUGUI coinAnimation;

    public int countEnemiesDead = 0;

    public bool isQuestForKill = false;

    public string enemiesTag;

    public bool isGatheringQuest;

    public string collectablesTag;

    public int gatheringQuantity = 0;

    public Font font;

    public AudioClip npcVoice;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(canCompletedQuest == true && Input.GetKeyDown(KeyCode.F) && haveQuest == true)
        {
            haveQuest = false;
            newQuestInstructionsUI.text = "";
            coinInInventory.text = (int.Parse(coinInInventory.text) + int.Parse(rewardQuest)).ToString();
            coinAnimation.text = rewardQuest + " Coins!";
            coinAnimation.gameObject.SetActive(true);
            coinAnimation.enabled = true;
            canCompletedQuest = false;
            informations.completed = true;
            StartCoroutine("waitForSecondsFunc");
            
        }

        if(canAcessQuest == true)
        {
            
            if (Input.GetKeyDown(KeyCode.F) && !questsLines.Contains(informations.questLine) && haveQuest == false && QuestUI.GetActive() == false)
            {
                QuestUI.SetActive(true);
                audioSource.PlayOneShot(npcVoice);
            }

        }

        if(canAcessQuest == false)
        {
            QuestUI.SetActive(false);
        }
     
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "NPC_QUEST")
        {
            

            if (haveQuest == false)
            {
                informations = other.GetComponent<QuestInformation>();

                if (informations.questLine == "quest1" || questsLines.Contains("quest1"))
                {
                    questDescriptionTextUI.text = informations.questDescription;
                    questRewardTextUI.text = informations.descriptionReward;
                    isQuestForKill = informations.killEnemies;
                    isGatheringQuest = informations.isGatheringQuest;
                    npcVoice = informations.npcVoice;
                    audioSource = informations.npcAudio;
                    canCompletedQuest = false;

                    if (isQuestForKill)
                    {
                        enemiesTag = informations.typeEnemies;
                        countEnemiesDead = informations.countEnemies;
                    }

                    if (isGatheringQuest)
                    {
                        collectablesTag = informations.collectablesTag;
                        gatheringQuantity = informations.gatheringQuantity;

                    }
                   

                    canAcessQuest = true;
                }

            }


            if (other.gameObject.name.Equals(informations.objective))
                {
                if (isQuestForKill)
                {
                    if(countEnemiesDead <= 0)
                    {
                        canCompletedQuest = true;
                    }
                }else if (isGatheringQuest)
                {
                    if(gatheringQuantity <= 0)
                    {
                        canCompletedQuest = true;
                    }
                }
                else
                {
                    canCompletedQuest = true;
                }
                }

            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "NPC_QUEST")
        {
            canAcessQuest = false;
        }
    }

    public void AcceptQuest()
    {
       
        QuestUI.SetActive(false);
        basicIntruction = informations.newInstructionsQuest;
        newQuestInstructionsUI.text = (isQuestForKill) ? informations.newInstructionsQuest + " - " + countEnemiesDead + " Enemies left" : informations.newInstructionsQuest;
        questsLines.Add(informations.questLine);
        rewardQuest = informations.valueInCoins;
        haveQuest = true;
    }

    public void DeclineQuest()
    {
        
        QuestUI.SetActive(false);
    }

    IEnumerator waitForSecondsFunc()
    {
        yield return new WaitForSeconds(0.5f);
        coinAnimation.enabled = false;
        coinAnimation.gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        if (canAcessQuest == true && QuestUI.GetActive() == false)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 10;
            GUI.skin.font = font;
            GUI.color = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Press 'F' to talk");
        }

    }
}
