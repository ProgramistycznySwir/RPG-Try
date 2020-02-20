using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APCSteering : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode handBrake = KeyCode.Space;


    public float maxSteeringAngle = 30f;
    public float steeringSpeed = 60f;
    public bool screwThis = true;

    public float maxEnginePower = 4000f;

    public AnimationCurve engineCurve;
    public float maxRPM = 300f;
    public float brakesPower = 10000f;

    [Tooltip("Rear/Fornt Wheel Drive")]
    public bool FWD, RWD;

    
    [Header("Wheels:")]
    public Transform steeringWheelsGroup;
    public Transform nonSteeringWheelsGroup;
    
    private WheelCollider[] steering;
    private WheelCollider[] nonSteering;

    [Tooltip("Fill it with wheels, order doesn't matter")]
    public Transform[] wheelModels;
    


    private float[] steeringAngles;
    private float[,] maxSteeringAngles; //0 - Left; 1 - Right
    private float throttle = 0;

    void Start()
    {
        AssignWheelGroups();

        steeringAngles = new float[steering.Length];
        maxSteeringAngles = new float[steering.Length, 2];

        CalculateSteeringAngles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left)) TurnLeft();
        else if (Input.GetKey(right)) TurnRight();
        else DontTurn();

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

        if (Input.GetKey(handBrake)) Brake();
    }

    void Steering()
    {
        for (int i = 0; i < steering.Length; i++)
        {
            steering[i].steerAngle = steeringAngles[i];
        }
    }
    void Accelerating()
    {
        foreach(WheelCollider wheel in nonSteering)
        {
            if(wheel.rpm * throttle < 0)
            {
                wheel.brakeTorque = brakesPower;
            }
            else wheel.brakeTorque = 0;
        }
        foreach (WheelCollider wheel in steering)
        {
            if (wheel.rpm * throttle < 0)
            {
                wheel.brakeTorque = brakesPower;
            }
            else wheel.brakeTorque = 0;
        }

        if (RWD)
        {
            foreach (WheelCollider wheel in nonSteering)
            {
                if (wheel.rpm < maxRPM)
                    wheel.motorTorque = maxEnginePower * throttle * engineCurve.Evaluate(Mathf.Abs(wheel.rpm) / maxRPM);
                else wheel.motorTorque = 0f;
            }
        }
    }

    void Brake()
    {
        foreach (WheelCollider wheel in steering)
        {
            wheel.brakeTorque = brakesPower;
        }
        foreach (WheelCollider wheel in nonSteering)
        {
            wheel.brakeTorque = brakesPower;
        }
    }

    void UpdateWheelsPositions()
    {
        for(int i = 0; i < wheelModels.Length; i++)
        {
            if(i < steering.Length)
            {
                UpdateWheelPosition(steering[i], wheelModels[i]);
            }
            else if(i - steering.Length < nonSteering.Length)
            {
                UpdateWheelPosition(nonSteering[i - steering.Length], wheelModels[i]);
            }
        }
    }
    void UpdateWheelPosition(WheelCollider collider, Transform model)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        model.position = position;
        model.rotation = rotation;
    }

    void TurnLeft()
    {
        for (int i = 0; i < steering.Length; i++)
        {
            //steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 0], Mathf.Abs(maxSteeringAngles[i, 0]) * Time.deltaTime);
            if (steeringAngles[i] >= 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 0], Mathf.Abs(maxSteeringAngles[i, 1] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
            else if (steeringAngles[i] < 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 0], Mathf.Abs(maxSteeringAngles[i, 0] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
        }
    }
    void TurnRight()
    {
        for (int i = 0; i < steering.Length; i++)
        {
            //steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 1], Mathf.Abs(maxSteeringAngles[i, 1]) * Time.deltaTime);
            if (steeringAngles[i] >= 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 1], Mathf.Abs(maxSteeringAngles[i, 1] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
            else if (steeringAngles[i] < 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], maxSteeringAngles[i, 1], Mathf.Abs(maxSteeringAngles[i, 0] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
        }
    }
    void DontTurn()
    {
        for (int i = 0; i < steering.Length; i++)
        {
            if (steeringAngles[i] > 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], 0, Mathf.Abs(maxSteeringAngles[i, 1] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
            else if (steeringAngles[i] < 0) steeringAngles[i] = Mathf.MoveTowardsAngle(steeringAngles[i], 0, Mathf.Abs(maxSteeringAngles[i, 0] / maxSteeringAngle) * Time.deltaTime * steeringSpeed);
        }
    }

    void CalculateSteeringAngles()
    {
        if (screwThis)
        {
            for(int ii = 0; ii < maxSteeringAngles.Length; ii++)
            {
                maxSteeringAngles[ii, 0] = -maxSteeringAngle;
                maxSteeringAngles[ii, 1] = maxSteeringAngle;
            }
            return;
        }
        Vector2 centerOfNonSteeringWheels = Vector2.zero;
        foreach (WheelCollider wheel in nonSteering)
        {
            centerOfNonSteeringWheels += ExtractXZ(wheel.transform.localPosition);
        }
        centerOfNonSteeringWheels /= nonSteering.Length;


        //Left
        int greatestAngleIndex = 0, i = 0;
        foreach (WheelCollider wheel in steering)
        {
            // + Vector2.right * 10f  - jest po to by wyciągnąć oś hipotetycznego skrętu poza pojazd
            if (Vector2.Angle(Vector2.right, ExtractXZ(wheel.transform.localPosition) + Vector2.right * 50f) > Vector2.Angle(Vector2.right, ExtractXZ(steering[greatestAngleIndex].transform.localPosition) + Vector2.right * 10f))
            {
                greatestAngleIndex = i;
            }
            i++;
        }

        float a = Mathf.Tan(Mathf.Deg2Rad * maxSteeringAngle);
        float b = steering[greatestAngleIndex].transform.localPosition.z / (a * steering[greatestAngleIndex].transform.localPosition.x);

        Vector2 centerOfSteeringLeft;
        centerOfSteeringLeft.y = centerOfNonSteeringWheels.y;

        centerOfSteeringLeft.x = (centerOfSteeringLeft.y - b) / a; //B.x = (B.y - b) / a

        for (i = 0; i < steering.Length; i++)
        {
            if (i == greatestAngleIndex)
            {
                maxSteeringAngles[i, 0] = -maxSteeringAngle; //note that '-'
            }
            else
            {
                maxSteeringAngles[i, 0] = -Vector2.Angle(Vector2.right, ExtractXZ(steering[i].transform.localPosition) - centerOfSteeringLeft);
            }
        }


        //Right

        greatestAngleIndex = 0; i = 0;
        foreach (WheelCollider wheel in steering)
        {
            // + Vector2.right * 10f  - jest po to by wyciągnąć oś hipotetycznego skrętu poza pojazd
            if (Vector2.Angle(Vector2.left, ExtractXZ(wheel.transform.localPosition) + Vector2.left * 50f) > Vector2.Angle(Vector2.left, ExtractXZ(steering[greatestAngleIndex].transform.localPosition) + Vector2.left * 10f))
            {
                greatestAngleIndex = i;
            }
            i++;
        }

        a = Mathf.Tan(Mathf.Deg2Rad * maxSteeringAngle);
        b = steering[greatestAngleIndex].transform.localPosition.z / (a * steering[greatestAngleIndex].transform.localPosition.x);

        Vector2 centerOfSteeringRight;
        centerOfSteeringRight.y = centerOfNonSteeringWheels.y;

        centerOfSteeringRight.x = (centerOfSteeringRight.y - b) / -a; //B.x = (B.y - b) / a

        for (i = 0; i < steering.Length; i++)
        {
            if (i == greatestAngleIndex)
            {
                maxSteeringAngles[i, 1] = maxSteeringAngle;
            }
            else
            {
                maxSteeringAngles[i, 1] = Vector2.Angle(Vector2.right, ExtractXZ(steering[i].transform.localPosition) - centerOfSteeringRight);
            }
        }
    }

    void AssignWheelGroups()
    {
        //leftTrack = new WheelCollider[leftTrackGroup.childCount];
        //rightTrack = new WheelCollider[rightTrackGroup.childCount];

        steering = steeringWheelsGroup.GetComponentsInChildren<WheelCollider>();
        nonSteering = nonSteeringWheelsGroup.GetComponentsInChildren<WheelCollider>();
    }

    static Vector2 ExtractXZ(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}
