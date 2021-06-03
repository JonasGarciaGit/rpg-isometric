using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowIA : MonoBehaviour
{

    public float followRadius = 10f;
    public float stopfollowRadius = 1f;
    public GameObject player;
    public GameObject objective;
    NavMeshAgent agent;
    public QuestSystem questSystem;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(objective);
        Debug.Log(player);

        if(questSystem.haveQuest == false)
        {
            Debug.Log("Zerei o objetivo");
            objective = null;
        }

        if(questSystem.isQuestForKill && questSystem.countEnemiesDead <= 0)
        {
            objective = GameObject.Find(questSystem.informations.objective);
        }

        if (questSystem.isGatheringQuest && questSystem.gatheringQuantity <= 0)
        {
            objective = GameObject.Find(questSystem.informations.objective);
        }


        if (questSystem.crowFollowQuest && questSystem.haveQuest)
        {

            if (questSystem.isQuestForKill && !questSystem.isGatheringQuest && questSystem.countEnemiesDead > 0)
            {
                objective = GameObject.FindGameObjectsWithTag(questSystem.enemiesTag)[0];
            }
            else if(!questSystem.isGatheringQuest && !questSystem.isQuestForKill)
            {
                objective = GameObject.Find(questSystem.informations.objective);
            }
        }

        if (objective && questSystem.crowFollowQuest == true)
        {

            float distanceObjective = Vector3.Distance(objective.transform.position, transform.position);
            float distancePlayer = Vector3.Distance(player.transform.position, transform.position);
            this.ChaseQuestObjective(distanceObjective, distancePlayer);
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= followRadius && questSystem.crowFollowQuest == false)
        {
            try
            {
                this.ChasePlayer(distance);
            }
            catch
            {

            }
            

        }

        if(distance > followRadius && questSystem.crowFollowQuest == false)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            agent.Warp(newPosition);
        }
        

    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void FaceQuestTarget()
    {
        Vector3 direction = (objective.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ChasePlayer(float distance)
    {
        try
        {
            Debug.Log("Player dentro do chase::" + player);
            if (distance <= stopfollowRadius)
            {

                Vector3 lookVector = player.transform.position - transform.position;
                lookVector.y = transform.position.y;
                Quaternion rot = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        }
        catch
        {
            Debug.Log("Nenhum alvo encontrado!");
        }

    }

    private void ChaseQuestObjective(float distance, float distancePlayer)
    {
        try
        {
            
            if (distancePlayer > followRadius)
            {
               
                Vector3 lookVector = objective.transform.position - transform.position;
                lookVector.y = transform.position.y;
                Quaternion rot = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

                agent.isStopped = true;
            }
            else
            {
                
                agent.isStopped = false;
                agent.SetDestination(objective.transform.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceQuestTarget();
                    //questSystem.crowFollowQuest = false;
                }
            }
        }
        catch
        {
            Debug.Log("Erro ao seguir inimigo");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopfollowRadius);
    }

}
