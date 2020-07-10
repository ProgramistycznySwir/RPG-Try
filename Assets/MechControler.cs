using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControler : MonoBehaviour
{
    public CharacterController movement;
    public HeadRotation head;
    public float topSpeed = 40f;
    public float acceleration = 40f;
    public float deceleration = 100f;
    public float rotationSpeed = 360f;
    public float jumpPower = 40f;
    public bool test;
    

    Vector2 input;
    float velocityV;
    Vector2 velocityH;
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
            test = !test;
        if(!test)
            transform.eulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(transform.eulerAngles.y, head.CameraH, rotationSpeed * Time.deltaTime), 0f);

        if (movement.isGrounded)
        {
            velocityV = -10f;
            if (Input.GetKeyDown(KeyCode.Space))
                velocityV = jumpPower;

            Vector2 normalizedInput = Vector2.zero;
            if(input.y != 0)
                normalizedInput += new Vector2(Mathf.Sin(head.CameraH * Mathf.Deg2Rad), Mathf.Cos(head.CameraH * Mathf.Deg2Rad)) * input.y;

            Debug.Log(Vector2.Dot(velocityH, input));
            if(input.x != 0)
            {
                if (Vector2.Dot(velocityH, input) <= 0)
                    normalizedInput += new Vector2(Mathf.Cos(head.CameraH * Mathf.Deg2Rad), -Mathf.Sin(head.CameraH * Mathf.Deg2Rad)) * input.x;
                else
                    normalizedInput += new Vector2(Mathf.Cos(head.CameraH * Mathf.Deg2Rad), -Mathf.Sin(head.CameraH * Mathf.Deg2Rad)) * input.x;
            }
            //else
                //normalizedInput += new Vector2(Mathf.Cos(head.CameraH * Mathf.Deg2Rad), -Mathf.Sin(head.CameraH * Mathf.Deg2Rad)) * input.x;


            velocityH += normalizedInput.normalized * acceleration * Time.deltaTime;
            velocityH = Vector2.ClampMagnitude(velocityH, topSpeed);
        }
        else
            velocityV += Physics.gravity.y * Time.deltaTime;

        movement.Move(new Vector3(velocityH.x, velocityV, velocityH.y) * Time.deltaTime);
    }
}
