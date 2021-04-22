using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyIA : MonoBehaviour
{

    public float lookRadius = 10f;
    public float atkRadius = 2f;
    private float attackCooldown;
    private bool isDie;
    

    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    public Transform startPosition;
    public Health monsterHealth;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.parent.gameObject.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attackCooldown = 1f;
        isDie = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (monsterHealth.currentHealth <= 0)
        {
            isDie = true;
            StartCoroutine("Die");
        }

        float distance = Vector3.Distance(target.position, transform.position);
        
        if (distance <= lookRadius && distance > atkRadius && animator.GetBool("isAttkOne") == false && isDie == false)
        {

            agent.SetDestination(target.position);

            if(animator.GetBool("isWalking") == false)
            {
                animator.SetBool("isWalking", true);
            }
           
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            
        }
        if(distance > lookRadius)
        {
            animator.SetBool("isWalking", true);
            agent.destination = startPosition.position;
            if (agent.velocity.sqrMagnitude == 0f)
            {
                animator.SetBool("isWalking", false);
            }
            

        }

        //Attacking 

        if(distance <= atkRadius)
        {
            
            if(attackCooldown == 1f)
            {

                int attkType = Random.Range(0, 10);
                if(attkType <= 5)
                {
                    FaceTarget();
                    StartCoroutine("attackOne");
                    attackCooldown = 0f;
                }

                if(attkType > 5)
                {
                    FaceTarget();
                    StartCoroutine("attackTwo");
                    attackCooldown = 0f;
                }
            }
            
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, atkRadius);
    }

    IEnumerator attackOne()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttkOne", true);

        yield return new WaitForSeconds(4f);

        agent.isStopped = false;
        animator.SetBool("isAttkOne", false);
        animator.SetBool("isWalking", true);
        attackCooldown = 1f;
    }

    IEnumerator attackTwo()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttkTwo", true);

        yield return new WaitForSeconds(4f);

        agent.isStopped = false;
        animator.SetBool("isAttkTwo", false);
        animator.SetBool("isWalking", true);
        attackCooldown = 1f;
    }

    IEnumerator Die()
    {
        if(isDie == true)
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttkTwo", false);
            animator.SetBool("isAttkOne", false);
            animator.SetBool("isDie", true);
        }


        yield return new WaitForSeconds(6f);

        Destroy(this.gameObject);
    }
}
