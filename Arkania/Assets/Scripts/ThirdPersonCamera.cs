using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 0.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private bool canMove = true;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 10.0f;
    private float sensitivityY = 1.0f;


	// Use this for initialization
	void Start () {
        camTransform = transform;
        cam = Camera.main;

	}
    void LateUpdate()
    {
        if (canMove)
        {
            Vector3 dir = new Vector3(-10.0f, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * -dir;
            camTransform.LookAt(lookAt.position);
        }

    }
    // Update is called once per frame
    void Update () {
        if (canMove)
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }

    }

    public void SetCanMove(bool a)
    {
        canMove = a;
    }
}
