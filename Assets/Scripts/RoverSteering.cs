using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSteering : MonoBehaviour
{
    public new Rigidbody rigidbody;

    public KeyCode handBreak = KeyCode.Space;
    public KeyCode boost = KeyCode.LeftShift;

    public float maxSpeed;
    public float power;
    public float brakingForce;

    public float boostPower;
    public float boostFuel;
    public float boostFuelTank;
    public float boostFuelRegeneration;

    public float maxSteeringAngle;
    public float steeringSpeed;

    public WheelCollider[] wheelsL;
    public WheelCollider[] wheelsR;

    public Transform[] modelsL;
    public Transform[] modelsR;


    public TMPro.TextMeshPro speedText;
    public TMPro.TextMeshPro boostText;


    float throttle;
    float steering;
    Vector3 wheelPosition;
    Quaternion wheelRotation;

    void Start()
    {
        boostFuel = boostFuelTank;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");

        if (Input.GetKey(handBreak))
        {
            wheelsL[1].brakeTorque = brakingForce;
            wheelsR[1].brakeTorque = brakingForce;
            //foreach (WheelCollider wheel in wheelsL)
            //    wheel.brakeTorque = brakingForce;
            //foreach (WheelCollider wheel in wheelsR)
            //    wheel.brakeTorque = brakingForce;
        }
        else
        {
            wheelsL[1].brakeTorque = 0f;
            wheelsR[1].brakeTorque = 0f;
            //foreach (WheelCollider wheel in wheelsL)
            //    wheel.brakeTorque = 0f;
            //foreach (WheelCollider wheel in wheelsR)
            //    wheel.brakeTorque = 0f;
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
                wheel.motorTorque = 0f;
            foreach (WheelCollider wheel in wheelsR)
                wheel.motorTorque = 0f;
        }

        if(Input.GetKey(boost))
        {
            if(boostFuel > 0f)
            {
                rigidbody.AddRelativeForce(Vector3.forward * boostPower);
                boostFuel = Mathf.MoveTowards(boostFuel, 0f, Time.fixedDeltaTime);
            }
        }
        else
            boostFuel = Mathf.MoveTowards(boostFuel, boostFuelTank, boostFuelRegeneration * Time.fixedDeltaTime);

        wheelsL[0].steerAngle = Mathf.MoveTowards(wheelsL[0].steerAngle, steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        wheelsR[0].steerAngle = Mathf.MoveTowards(wheelsR[0].steerAngle, steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        //wheelsL[2].steerAngle = Mathf.MoveTowards(wheelsL[2].steerAngle, -steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);
        //wheelsR[2].steerAngle = Mathf.MoveTowards(wheelsR[2].steerAngle, -steering * maxSteeringAngle, steeringSpeed * Time.fixedDeltaTime);

        UpdateWheelPositions();
        Display();
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

    void Display()
    {
        speedText.text = (rigidbody.velocity.magnitude * 3.6f).ToString("F0") + "km/h";
        boostText.text = ((boostFuel / boostFuelTank) * 100f).ToString("F0") + "%";
    }
}
