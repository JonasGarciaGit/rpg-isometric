using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public Text nickname;

    public Text characterName;

    public float jumpForce = 2.0f;

    public Vector3 jump;

    public Interactable focus;

    public AudioSource audioSource;

    public AudioClip lightAttack;

    public AudioClip heavyAttack_1;

    public AudioClip heavyAttack_2;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();

        if (photonView.isMine)
        {
            camera.SetActive(true);
            weapon.SetActive(false);

            Debug.Log(photonView.owner.NickName.ToString());

            nickname.text = photonView.owner.NickName;
            characterName.text = photonView.owner.NickName;

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
                audioSource.volume = 0.5f;
                audioSource.clip = lightAttack;
                audioSource.PlayDelayed(0.3f);
                StartCoroutine("PlayerAttack");
            }

            if (Input.GetMouseButtonDown(1) && animator.GetBool("isGrounded") == true)
            {
                StartCoroutine("PlayerAttackHeavy");
            }

            if(Input.GetKey(KeyCode.Space) && animator.GetBool("isGrounded") == true 
                && animator.GetBool("isRunning") == false
                && animator.GetBool("isAttacking") == false
                && animator.GetBool("isAttackingHeavying") == false)
            {
                StartCoroutine("PlayerJumpingStand");
            }


      
        }

    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.Space) && animator.GetBool("isGrounded") == true
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
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();


            focus = newFocus;
        }

        focus = newFocus;
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();

        focus = null;
    }
  
    private void RotateTowardsMovementVector(Vector3 movementVector)
    {
        try
        {
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

        targetVector = Quaternion.Euler(0, camera.transform.eulerAngles.y , 0) * targetVector;
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
             && animator.GetBool("isAttackingHeavying") == false)
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
        weapon.SetActive(true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        this.moveSpeed = 0f;
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttacking", false);
        weapon.SetActive(false);
        this.moveSpeed = 4f;

    }

    IEnumerator PlayerAttackHeavy()
    {
        weapon.SetActive(true);
        animator.SetBool("isGrounded", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttackingHeavying", true);
        this.moveSpeed = 0f;
        yield return new WaitForSeconds(2f);
        weapon.SetActive(false);
        animator.SetBool("isAttackingHeavying", false);
        this.moveSpeed = 4f;
    }

}


