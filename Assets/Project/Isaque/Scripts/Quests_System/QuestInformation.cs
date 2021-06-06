using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestInformation : MonoBehaviour
{
    // Start is called before the first frame update
    [TextArea]
    public string questDescription;
    public string descriptionReward;
    public string valueInCoins;
    public string newInstructionsQuest;
    public string questLine;
    public string objective;
    public int rankExp;
    public AudioClip npcVoice;
    public AudioSource npcAudio;
    
    //Variaveis utilizadas para quests onde devemos matar
    public bool killEnemies;
    public string typeEnemies;
    public int countEnemies;

    //Variaveis utilizadas para quests onde devemos coletar
    public bool isGatheringQuest;
    public string collectablesTag;
    public int gatheringQuantity;

    public bool completed = false;
    public QuestInformation newQuest;

    //private bool haveQuest = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (completed && newQuest != null)
        {
            questDescription = newQuest.questDescription;
            descriptionReward = newQuest.descriptionReward;
            valueInCoins = newQuest.valueInCoins;
            newInstructionsQuest = newQuest.newInstructionsQuest;
            questLine = newQuest.questLine;
            objective = newQuest.objective;
            rankExp = newQuest.rankExp;

            killEnemies = newQuest.killEnemies;
            typeEnemies = newQuest.typeEnemies;
            countEnemies = newQuest.countEnemies;

            isGatheringQuest = newQuest.isGatheringQuest;
            collectablesTag = newQuest.collectablesTag;
            gatheringQuantity = newQuest.gatheringQuantity;
            npcVoice = newQuest.npcVoice;
            npcAudio = newQuest.npcAudio;

            completed = false;
            newQuest = newQuest.newQuest;
        }
    }
}
