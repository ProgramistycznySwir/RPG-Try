using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSteering : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode handBrake = KeyCode.Space;

    public float maxEnginePower = 1500f;

    public AnimationCurve engineCurve;
    public float maxRPM = 10f;

    public float brakesPower = 4000f;


    [Header("Tracks:")]
    private WheelCollider[] leftTrack;
    private WheelCollider[] rightTrack;

    [Tooltip("Parent of all WheelColliders of left track")]
    public Transform leftTrackGroup;
    [Tooltip("Parent of all WheelColliders of right track")]
    public Transform rightTrackGroup;


    private float leftThrottle = 0;
    private float rightThrottle = 0;

    void Start()
    {
        AssignWheelGroups();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(leftForward)) leftThrottle = 1;
        //else if (Input.GetKey(leftBackwards)) leftThrottle = -1;
        //else leftThrottle = 0;

        //if (Input.GetKey(rightForward)) rightThrottle = 1;
        //else if (Input.GetKey(rightBackwards)) rightThrottle = -1;
        //else rightThrottle = 0;

        if (Input.GetKey(forward))
        {
            if (Input.GetKey(left)) { leftThrottle = 0; rightThrottle = 1; brakes = BrakesMode.Left; }
            else if (Input.GetKey(right)) { leftThrottle = 1; rightThrottle = 0; brakes = BrakesMode.Right; }
            else { leftThrottle = 1; rightThrottle = 1; brakes = BrakesMode.None; }
        }
        else if (Input.GetKey(backwards))
        {
            if (Input.GetKey(left)) { leftThrottle = 0; rightThrottle = -1; brakes = BrakesMode.Left; }
            else if (Input.GetKey(right)) { leftThrottle = -1; rightThrottle = 0; brakes = BrakesMode.Right; }
            else { leftThrottle = -1; rightThrottle = -1; brakes = BrakesMode.None; }
        }
        else
        {
            if (Input.GetKey(left)) { leftThrottle = -1; rightThrottle = 1; brakes = BrakesMode.None; }
            else if (Input.GetKey(right)) { leftThrottle = 1; rightThrottle = -1; brakes = BrakesMode.None; }
            else { leftThrottle = 0; rightThrottle = 0; brakes = BrakesMode.None; }
        }
        if(Input.GetKey(handBrake)) brakes = BrakesMode.Both;

        Brakes();

        Propulsion();
        UpdateWheelsPositions();
    }
    
    void Propulsion()
    {
        foreach(WheelCollider wheel in leftTrack)
        {
            if (Mathf.Abs(wheel.rpm) < maxRPM)
                wheel.motorTorque = maxEnginePower * leftThrottle * engineCurve.Evaluate(Mathf.Abs(wheel.rpm) / maxRPM);
            else wheel.motorTorque = 0;
        }
        foreach (WheelCollider wheel in rightTrack)
        {
            if (Mathf.Abs(wheel.rpm) < maxRPM)
                wheel.motorTorque = maxEnginePower * rightThrottle * engineCurve.Evaluate(Mathf.Abs(wheel.rpm) / maxRPM);
            else wheel.motorTorque = 0;
        }
    }

    private enum BrakesMode { None, Left, Right, Both }
    private BrakesMode brakes;
    void Brakes()
    {
        foreach (WheelCollider wheel in leftTrack)
        {
            if (brakes == BrakesMode.Left || brakes == BrakesMode.Both) wheel.brakeTorque = brakesPower;
            else wheel.brakeTorque = 0;
        }
        foreach (WheelCollider wheel in rightTrack)
        {
            if (brakes == BrakesMode.Right || brakes == BrakesMode.Both) wheel.brakeTorque = brakesPower;
            else wheel.brakeTorque = 0;
        }
    }

    void UpdateWheelsPositions()
    {
        
    }
    void UpdateWheelPosition(WheelCollider collider, Transform model)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        model.position = position;
        model.rotation = rotation;
    }

    void AssignWheelGroups()
    {
        //leftTrack = new WheelCollider[leftTrackGroup.childCount];
        //rightTrack = new WheelCollider[rightTrackGroup.childCount];

        leftTrack = leftTrackGroup.GetComponentsInChildren<WheelCollider>();
        rightTrack = rightTrackGroup.GetComponentsInChildren<WheelCollider>();
    }
}
