using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public float maxSteeringAngle = 30f;
    public float steeringSpeed = 60f;

    public float enginePower = 50f;

    [Tooltip("Rear/Fornt Wheel Drive")]
    public bool FWD, RWD;


    [Header("Wheels:")]
    public WheelCollider FLWheel;
    public WheelCollider FRWheel;
    public WheelCollider RLWheel;
    public WheelCollider RRWheel;

    public Transform FL, FR;
    public Transform RL, RR;

    


    private float steeringAngle = 0;
    private float throttle = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
        {
            steeringAngle -= steeringSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(right) || steeringAngle < -steeringSpeed * Time.deltaTime)
        {
            steeringAngle += steeringSpeed * Time.deltaTime;
        }
        else if(steeringAngle > steeringSpeed * Time.deltaTime)
        {
            steeringAngle -= steeringSpeed * Time.deltaTime;
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -maxSteeringAngle, maxSteeringAngle);

        if (Input.GetKey(forward))
        {
            throttle = 1;
        }
        else if (Input.GetKey(backwards))
        {
            throttle = -1;
        }
        else throttle = 0;

        Steering();
        Accelerating();
        UpdateWheelsPositions();
    }

    void Steering()
    {
        FLWheel.steerAngle = steeringAngle;
        FRWheel.steerAngle = steeringAngle;
    }
    void Accelerating()
    {
        if (RWD)
        {
            RLWheel.motorTorque = enginePower * throttle;
            RRWheel.motorTorque = enginePower * throttle;
        }
    }
    void UpdateWheelsPositions()
    {
        UpdateWheelPosition(FLWheel, FL);
        UpdateWheelPosition(FRWheel, FR);
        UpdateWheelPosition(RLWheel, RL);
        UpdateWheelPosition(RRWheel, RR);
    }
    void UpdateWheelPosition(WheelCollider collider, Transform model)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        model.position = position;
        model.rotation = rotation;
    }
}
