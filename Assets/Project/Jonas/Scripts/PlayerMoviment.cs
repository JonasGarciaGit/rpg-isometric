using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    private InputHandler _input;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Camera camera;


    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //move na direção que estamos olhando
        var movementVector = MoveTowardTarget(targetVector);

        //rotaciona na direção que estamos olhando
        RotateTowardsMovementVector(movementVector);

    }

    private void RotateTowardsMovementVector(Vector3 movementVector)
    {
        if(movementVector.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y , 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
}
