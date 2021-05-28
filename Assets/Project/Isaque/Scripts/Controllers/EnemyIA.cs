using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class EnemyIA : MonoBehaviour
{

    public float lookRadius = 10f;
    public float atkRadius = 2f;
    private float attackCooldown;
    private bool isDie;
    public int monsterExp;
    private bool animationHasFineshed;

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

    [SerializeField]
    private GameObject skillOne;
    [SerializeField]
    private GameObject skillTwo;
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

                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius && animator.GetBool("isAttkOne") == false && animator.GetBool("isAttkTwo") == false && isDie == false)
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


        if (this.gameObject.tag == "Witch" && animator.GetBool("isDie") == false)
        {
            Invoke("instatiateSkillOne", 1.37f);
        }

        yield return new WaitForSeconds(4f);


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


        if (this.gameObject.tag == "Witch" && animator.GetBool("isDie") == false)
        {
            Invoke("instatiateSkillTwo", 1f);
        }

        yield return new WaitForSeconds(4f);


        agent.isStopped = false;
        animator.SetBool("isAttkTwo", false);
        animator.SetBool("isWalking", true);
       

        yield return new WaitForSeconds(1f);

        attackCooldown = 1f;
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

    [PunRPC]
    private void instatiateSkillOne()
    {

        GameObject skill = Instantiate(skillOne, weaponDamage.myTransform.position, Quaternion.identity);
        AudioSource audioSrc = skill.AddComponent<AudioSource>();
        audioSrc.minDistance = 2f;
        audioSrc.maxDistance = 2.1f;
        audioSrc.volume = 0.2f;
        audioSrc.clip = skillEffectsSound;
        audioSrc.Play();
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 5f;
        Destroy(skill, 5f);
    }

    [PunRPC]
    private void instatiateSkillTwo()
    {

        GameObject skill = Instantiate(skillTwo, weaponDamage.myTransform.position, Quaternion.identity);
        AudioSource audioSrc = skill.AddComponent<AudioSource>();
        audioSrc.minDistance = 1f;
        audioSrc.maxDistance = 10f;
        audioSrc.spatialBlend = 1f;
        audioSrc.rolloffMode = AudioRolloffMode.Linear;
        audioSrc.volume = 0.2f;
        audioSrc.clip = skillEffectsSound;
        audioSrc.Play();
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 5f;
        Destroy(skill, 5f);
    }
}
