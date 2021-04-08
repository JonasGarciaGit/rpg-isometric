using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : Photon.MonoBehaviour
{

    public PhotonView photonView;

    private InputHandler _input;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private GameObject camera;


    private void Awake()
    {
        _input = GetComponent<InputHandler>();

        if (photonView.isMine)
        {
            camera.SetActive(true);
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
            photonView.RPC("RotateTowardsMovementVector", PhotonTargets.AllBuffered, movementVector);

            PlayerRunning();

        }

    }


    [PunRPC]
    private void RotateTowardsMovementVector(Vector3 movementVector)
    {
        if(movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);


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
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && animator.GetBool("isGrounded") == true)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }


}
