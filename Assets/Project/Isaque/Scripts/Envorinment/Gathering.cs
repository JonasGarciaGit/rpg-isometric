using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gathering : MonoBehaviour
{

    private bool canGathering;
    public Font font;
    private QuestSystem questSystem;
    private Animator playerAnimator;
    private bool canExecuteAgain = true;
    private bool coroutineGathering = false;
    private PlayerMoviment playerMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canGathering == true)
        {

            if(questSystem.gatheringQuantity > 0 && coroutineGathering == false)
            {
                StartCoroutine("GatheringIEnum");
            }
            
        }


    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {

            questSystem = other.GetComponent<QuestSystem>();
            playerMove = other.GetComponent<PlayerMoviment>();
            playerAnimator = questSystem.GetComponentInParent<Animator>();

            if (questSystem.isGatheringQuest == true && questSystem.haveQuest == true)
            {

                if (questSystem.collectablesTag == this.gameObject.tag)
                {
                    canGathering = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canGathering = false;
        }
    }

    private void OnGUI()
    {
        if (canGathering == true)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 10;
            GUI.skin.font = font;
            GUI.color = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Press 'F' to gathering");
        }

    }

    IEnumerator GatheringIEnum()
    {

        coroutineGathering = true;

        if (canExecuteAgain)
        {
            playerAnimator.Play("Gathering");
            playerMove.moveSpeed = 0;
        }
        

        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gathering") && canExecuteAgain == true)
        {

            
            if(canExecuteAgain == true)
            {
                questSystem.gatheringQuantity = questSystem.gatheringQuantity - 1;
            }
            

            canExecuteAgain = false;

            if (questSystem.gatheringQuantity <= 0)
            {
                questSystem.newQuestInstructionsUI.text = "Quest completed! Deliver the quest to receive the rewards.";
            }
            else
            {
                questSystem.newQuestInstructionsUI.text = questSystem.basicIntruction + " - left " + questSystem.gatheringQuantity + " " + this.gameObject.tag + " to completed";
            }
        }

        yield return new WaitForSeconds(5f);

        playerAnimator.Play("Idle");
        canExecuteAgain = true;
        coroutineGathering = false;
        playerMove.moveSpeed = 4;
    }
}

