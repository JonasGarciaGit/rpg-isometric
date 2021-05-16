using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Transform playerTransform;

    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    public bool lookAtPlayer = false;

    public bool rotateAroundPlayer = false;

    public float rotationsSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (rotateAroundPlayer)
        {
            Quaternion canTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationsSpeed, Vector3.up);

            cameraOffset = canTurnAngle * cameraOffset;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            rotateAroundPlayer = true;
        }
        else
        {
            rotateAroundPlayer = false;
        }

        Vector3 newPos = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (lookAtPlayer || rotateAroundPlayer)
        {
            transform.LookAt(playerTransform);
        }
    }
}
