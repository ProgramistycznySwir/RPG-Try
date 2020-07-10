using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControler : MonoBehaviour
{
    public CharacterController movement;
    public HeadRotation head;
    public float speed = 10f;
    public float rotationSpeed = 360f;
    public float jumpPower = 40f;
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 input;
    Vector3 velocity;
    Vector3 movementVelocity;
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        transform.eulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(transform.eulerAngles.y, head.CameraH, rotationSpeed * Time.deltaTime), 0f);

        if (movement.isGrounded)
        {            
            velocity.y = -10f;
            if (Input.GetKeyDown(KeyCode.Space))
                velocity.y = jumpPower;

            Vector3 normalizedInput = Vector3.zero;
            normalizedInput += new Vector3(Mathf.Sin(head.CameraH * Mathf.Deg2Rad), 0f, Mathf.Cos(head.CameraH * Mathf.Deg2Rad)) * input.y;
            normalizedInput += new Vector3(Mathf.Cos(head.CameraH * Mathf.Deg2Rad), 0f, -Mathf.Sin(head.CameraH * Mathf.Deg2Rad)) * input.x;

            movementVelocity = normalizedInput.normalized * speed;
        }
        else
            velocity += Physics.gravity * Time.deltaTime;

        movement.Move((velocity + movementVelocity) * Time.deltaTime);
    }
}
