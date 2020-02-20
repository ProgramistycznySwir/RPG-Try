using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSteering : MonoBehaviour
{
    public new Rigidbody rigidbody;

    public KeyCode handBreak = KeyCode.Space;

    public float maxSpeed;
    public float power;
    public float brakingForce;

    public float maxSteeringAngle;
    public float steeringSpeed;

    public WheelCollider[] wheelsL;
    public WheelCollider[] wheelsR;

    public Transform[] modelsL;
    public Transform[] modelsR;


    public TMPro.TextMeshPro speedText;


    float throttle;
    float steering;
    Vector3 wheelPosition;
    Quaternion wheelRotation;

    void Start()
    {
        rigidbody.centerOfMass = new Vector3(0f, -1f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");

        if (Input.GetKey(handBreak))
        {
            foreach (WheelCollider wheel in wheelsL)
                wheel.brakeTorque = brakingForce;
            foreach (WheelCollider wheel in wheelsR)
                wheel.brakeTorque = brakingForce;
        }
        else
        {
            foreach (WheelCollider wheel in wheelsL)
                wheel.brakeTorque = 0f;
            foreach (WheelCollider wheel in wheelsR)
                wheel.brakeTorque = 0f;
        }
        
        if(rigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
        {
            foreach (WheelCollider wheel in wheelsL)
                wheel.motorTorque = power * throttle;
            foreach (WheelCollider wheel in wheelsR)
                wheel.motorTorque = power * throttle;
        }
        else
        {
            foreach (WheelCollider wheel in wheelsL)
                wheel.motorTorque = 0;
            foreach (WheelCollider wheel in wheelsR)
                wheel.motorTorque = 0;
        }

        wheelsL[0].steerAngle = Mathf.MoveTowards(wheelsL[0].steerAngle, steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        wheelsR[0].steerAngle = Mathf.MoveTowards(wheelsR[0].steerAngle, steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        //wheelsL[2].steerAngle = Mathf.MoveTowards(wheelsL[2].steerAngle, -steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        //wheelsR[2].steerAngle = Mathf.MoveTowards(wheelsR[2].steerAngle, -steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);

        UpdateWheelPositions();

        speedText.text = (rigidbody.velocity.magnitude * 3.6f).ToString("F0") + "km/h";
    }

    void UpdateWheelPositions()
    {
        for (int i = 0; i < 2; i++)
        {
            wheelsL[i].GetWorldPose(out wheelPosition, out wheelRotation);
            modelsL[i].position = wheelPosition;
            modelsL[i].rotation = wheelRotation;

            wheelsR[i].GetWorldPose(out wheelPosition, out wheelRotation);
            modelsR[i].position = wheelPosition;
            modelsR[i].rotation = wheelRotation;
        }
    }
}
