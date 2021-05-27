using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMoviment : Photon.MonoBehaviour
{

    public PhotonView photonView;

    private InputHandler _input;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rigidbody;

    public float moveSpeed = 4f;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private GameObject weapon;


    [SerializeField]
    private GameObject weaponTwo;

    public Text nickname;

    public Text characterName;

    public float jumpForce = 2.0f;

    public Vector3 jump;

    public Interactable focus;

    public AudioSource audioSource;

    public string characterClass;

    private bool isAttacking = false;

    private bool anotherPlayerAttacking = false;

    private string anotherPlayerNickname;

    private AudioListener audioListener;

    private void Awake()
    {
        audioListener = gameObject.GetComponent<AudioListener>();
        _input = GetComponent<InputHandler>();

        if (photonView.isMine)
        {
            camera.SetActive(true);
            weapon.SetActive(false);
            audioListener.enabled = true;

            if (weaponTwo != null)
            {
                weaponTwo.SetActive(false);
            }


            Debug.Log(photonView.owner.NickName.ToString());

            nickname.text = photonView.owner.NickName;
            characterName.text = photonView.owner.NickName;

        }

        if (!photonView.isMine)
        {
            weapon.SetActive(false);

            if (weaponTwo != null)
            {
                weaponTwo.SetActive(false);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.isMine)
        {

            var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

            //move na direção que estamos olhando
            var movementVector = MoveTowardTarget(targetVector);

            //rotaciona na direção que estamos olhando


            //photonView.RPC("RotateTowardsMovementVector", PhotonTargets.AllBuffered, movementVector);

            RotateTowardsMovementVector(movementVector);


            //Animações do jogador

            PlayerRunning();

            if (Input.GetKeyDown(KeyCode.Space)
                && animator.GetBool("isGrounded") == true
                && animator.GetBool("isRolling") == false
                && animator.GetBool("isRunningJumping") == false
                && !isAttacking)
            {
                StartCoroutine("PlayerRolling");
            }


            if (EventSystem.current.IsPointerOverGameObject())
                return;


            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        return;
                    }
                    else
                    {
                        RemoveFocus();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && animator.GetBool("isGrounded") == true)
            {
                if (!isAttacking)
                {
                    StartCoroutine("PlayerAttack");
                }

            }

            if (Input.GetMouseButtonDown(1) && animator.GetBool("isGrounded") == true)
            {
                if (!isAttacking)
                {
                    StartCoroutine("PlayerAttackHeavy");
                }

            }

            if (Input.GetKey(KeyCode.X) && animator.GetBool("isGrounded") == true
                && animator.GetBool("isRunning") == false
                && animator.GetBool("isAttacking") == false
                && animator.GetBool("isAttackingHeavying") == false)
            {
                StartCoroutine("PlayerJumpingStand");
            }
        }
        else
        {
            if(anotherPlayerAttacking && anotherPlayerNickname.Equals(photonView.owner.NickName))
            {
                weapon.SetActive(true);

                if (weaponTwo != null)
                {
                    weaponTwo.SetActive(true);
                }
            }
            else
            {
                weapon.SetActive(false);

                if (weaponTwo != null)
                {
                    weaponTwo.SetActive(false);
                }
            }

        }
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.X) && animator.GetBool("isGrounded") == true
            && animator.GetBool("isRunning") == true
            && animator.GetBool("isAttacking") == false
            && animator.GetBool("isAttackingHeavying") == false)
        {
            rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            StartCoroutine("PlayerJumpingRunning");
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();


            focus = newFocus;
        }

        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
    }

    private void RotateTowardsMovementVector(Vector3 movementVector)
    {
        try
        {
            if (animator.GetBool("isAttacking") ||
                animator.GetBool("isAttackingHeavying") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Death") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Gathering"))

            {
                return;
            }


            if (movementVector.magnitude == 0) { return; }
            var rotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }


    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }


    //Funções para controllar a animação do jogador
    private void PlayerRunning()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            && animator.GetBool("isGrounded") == true
            && animator.GetBool("isAttacking") == false
             && animator.GetBool("isAttackingHeavying") == false
             && animator.GetBool("isRolling") == false)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            audioSource.Stop();
            animator.SetBool("isRunning", false);
        }
    }

    IEnumerator PlayerJumpingRunning()
    {
        animator.SetBool("isGrounded", false);
        animator.SetBool("isRunningJumping", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunningJumping", false);

    }

    IEnumerator PlayerRolling()
    {
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRolling", true);
        animator.SetBool("isRunning", false);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRolling", false);

    }

    IEnumerator PlayerJumpingStand()
    {
        this.moveSpeed = 0f;
        animator.SetBool("isGrounded", false);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isGrounded", true);
        this.moveSpeed = 4f;
    }

    IEnumerator PlayerAttack()
    {
        if (weapon.active == false)
        {
            weapon.SetActive(true);
            if (weaponTwo != null)
            {
                weaponTwo.SetActive(true);
            }

        }

        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        this.moveSpeed = 0f;
        isAttacking = true;
        photonView.RPC("checkAnotherPlayerAttack", PhotonTargets.AllBuffered, isAttacking, photonView.owner.NickName);
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        photonView.RPC("checkAnotherPlayerAttack", PhotonTargets.AllBuffered, isAttacking, photonView.owner.NickName);
        animator.SetBool("isAttacking", false);
        this.moveSpeed = 4f;


        weapon.SetActive(false);

        if (weaponTwo != null)
        {
            weaponTwo.SetActive(false);
        }
    }

    IEnumerator PlayerAttackHeavy()
    {
        if (weapon.active == false)
        {
            weapon.SetActive(true);

            if (weaponTwo != null)
            {
                weaponTwo.SetActive(true);
            }

        }

        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttackingHeavying", true);
        this.moveSpeed = 0f;
        isAttacking = true;
        photonView.RPC("checkAnotherPlayerAttack", PhotonTargets.AllBuffered, isAttacking, photonView.owner.NickName);
        yield return new WaitForSeconds(characterClass.Equals("Warrior") ? 1.5f : characterClass.Equals("Knight") ? 2f : characterClass.Equals("Viking") ? 2.5f : 1.5f);
        isAttacking = false;
        photonView.RPC("checkAnotherPlayerAttack", PhotonTargets.AllBuffered, isAttacking, photonView.owner.NickName);
        animator.SetBool("isAttackingHeavying", false);
        this.moveSpeed = 4f;

        weapon.SetActive(false);

        if (weaponTwo != null)
        {
            weaponTwo.SetActive(false);
        }

    }

    [PunRPC]
    public void checkAnotherPlayerAttack(bool value, string nickname)
    {
        this.anotherPlayerAttacking = value;
        this.anotherPlayerNickname = nickname;
    }

}


