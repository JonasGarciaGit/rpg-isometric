using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class BossIA : MonoBehaviour
{
    public float lookRadius = 10f;
    public float atkRadius = 2f;
    private float attackCooldown;
    private bool isDie;
    public int monsterExp;
    public bool canUseSpecial = false;
    public GameObject throwRock;
    public float impulseRockForce = 3.0f;

    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    GameObject[] targets;
    GameObject t;

    [SerializeField]
    private Animator animator;

    public Transform startPosition;
    public Health monsterHealth;
    public DealSomeDamage weaponDamage;

    private List<string> specials = new List<string>();

    [SerializeField]
    private AudioClip skillEffectsSound;

    [SerializeField]
    private GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.isMine)
        {
            startPosition = this.gameObject.transform.parent.gameObject.transform;
            agent = GetComponent<NavMeshAgent>();
            attackCooldown = 1f;
            isDie = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {

            try
            {
                if (target == null)
                {
                    seachTarget();
                }



                if (monsterHealth.currentHealth <= 0)
                {
                    isDie = true;
                    StartCoroutine("Die");
                }

                if(monsterHealth.currentHealth <= (monsterHealth.maxHealth - (monsterHealth.maxHealth / 4)) && !specials.Contains("specialOne"))
                {
                    canUseSpecial = true;
                    specials.Add("specialOne");
                }
                if(monsterHealth.currentHealth <= monsterHealth.maxHealth / 2 && !specials.Contains("specialTwo"))
                {
                    canUseSpecial = true;
                    specials.Add("specialTwo");
                }
                if(monsterHealth.currentHealth <= monsterHealth.maxHealth / 4 && !specials.Contains("specialThree"))
                {
                    canUseSpecial = true;
                    specials.Add("specialThree");
                }

                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius && animator.GetBool("isAttkOne") == false && animator.GetBool("isAttkTwo") == false && animator.GetBool("isSpecial") == false && isDie == false)
                {

                    photonView.RPC("ChasePlayer", PhotonTargets.AllBuffered, distance);

                }
                if (distance > lookRadius)
                {
                    try
                    {
                        target = null;
                        animator.SetBool("isWalking", true);
                        agent.destination = startPosition.position;
                        specials = new List<string>();

                        if (monsterHealth.currentHealth < monsterHealth.maxHealth)
                        {
                            monsterHealth.ModifyHealth(20);
                            if (monsterHealth.currentHealth > monsterHealth.maxHealth)
                            {
                                int adjustLife = monsterHealth.maxHealth - monsterHealth.currentHealth;
                                monsterHealth.ModifyHealth(-adjustLife);
                            }

                        }

                        if (agent.velocity.sqrMagnitude == 0f)
                        {
                            animator.SetBool("isWalking", false);
                        }
                    }
                    catch
                    {

                    }
                }

                //Attacking 

                if (distance <= atkRadius)
                {

                    if (attackCooldown == 1f)
                    {
                        if(!canUseSpecial == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("Special"))
                        {
                            int attkType = Random.Range(0, 10);
                            if (attkType <= 5)
                            {
                                StartCoroutine("attackOne");
                                attackCooldown = 0f;
                            }

                            if (attkType > 5)
                            {
                                StartCoroutine("attackTwo");
                                attackCooldown = 0f;
                            }

                        }
                        else
                        {
                            StartCoroutine("Special");
                            attackCooldown = 0f;
                        }

                    }

                }




            }
            catch
            {

            }
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    [PunRPC]
    private void ChasePlayer(float distance)
    {
        try
        {
            if (distance <= atkRadius && gameObject.tag.Equals("Witch"))
            {
                //Fazer com que a bruxa olhe na direção do alvo
                Vector3 lookVector = target.transform.position - transform.position;
                lookVector.y = transform.position.y;
                Quaternion rot = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

                agent.isStopped = true;
                animator.SetBool("isWalking", false);
            }
            else
            {

                agent.isStopped = false;
                animator.SetBool("isWalking", true);
                agent.SetDestination(target.position);

                if (animator.GetBool("isWalking") == false)
                {
                    animator.SetBool("isWalking", true);
                }

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


        yield return new WaitForSeconds(4.28f);


        agent.isStopped = false;
        animator.SetBool("isAttkOne", false);
        animator.SetBool("isWalking", true);

        yield return new WaitForSeconds(1f);

        attackCooldown = 1f;


    }

    IEnumerator attackTwo()
    {

        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttkTwo", true);

        yield return new WaitForSeconds(2.33f);


        agent.isStopped = false;
        animator.SetBool("isAttkTwo", false);
        animator.SetBool("isWalking", true);


        yield return new WaitForSeconds(1f);

        attackCooldown = 1f;
    }

    IEnumerator Special()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isSpecial", true);
        int tempDamage = weaponDamage.weaponDamage;
        weaponDamage.weaponDamage = weaponDamage.weaponDamage * 2;

        yield return new WaitForSeconds(3f);

        agent.isStopped = false;
        animator.SetBool("isSpecial", false);
        animator.SetBool("isWalking", true);
        animator.Play("idle");
        weaponDamage.weaponDamage = tempDamage;

        yield return new WaitForSeconds(1f);

        attackCooldown = 1f;
        canUseSpecial = false;

    }


    IEnumerator Die()
    {
        if (isDie == true)
        {

            weaponDamage.weaponDamage = 0;
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttkTwo", false);
            animator.SetBool("isAttkOne", false);
            animator.SetBool("isDie", true);
        }


        yield return new WaitForSeconds(6f);

        GameObject coin = Instantiate(coinPrefab, this.gameObject.transform.position, Quaternion.identity);
        coin.GetComponent<PickUpCoin>().monsterType = this.gameObject.tag;
        Destroy(this.gameObject);
    }


    private void seachTarget()
    {
        try
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            t = targets.OrderBy(go => (this.transform.position - go.transform.position).sqrMagnitude).First();
            target = t.transform;
        }
        catch
        {

        }

    }

    public void throwRockUp()
    {

        GameObject rock = Instantiate(throwRock,new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f) , Quaternion.identity);
        Rigidbody rb = rock.GetComponent<Rigidbody>();
        float calculateX = Random.Range(0, 2);
        float calculateZ = Random.Range(0, 2);
        Vector3 impulseRock = new Vector3(calculateX, 4f, calculateZ);
        rb.AddForce(impulseRock * impulseRockForce, ForceMode.Impulse);
        Destroy(rock, 5f);
    }


}
