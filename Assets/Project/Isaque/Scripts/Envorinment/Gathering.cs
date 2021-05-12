using UnityEngine;

public class Gathering : MonoBehaviour
{

    private bool canGathering;
    public Font font;
    private QuestSystem questSystem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canGathering == true)
        {

            questSystem.gatheringQuantity = questSystem.gatheringQuantity - 1;


            if (questSystem.gatheringQuantity <= 0)
            {
                questSystem.newQuestInstructionsUI.text = "Quest completed! Deliver the quest to receive the rewards.";
            }
            else
            {
                questSystem.newQuestInstructionsUI.text = questSystem.basicIntruction + " - left " + questSystem.gatheringQuantity + " " + this.gameObject.tag + " to completed";
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            
            questSystem = other.GetComponent<QuestSystem>();


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
}
