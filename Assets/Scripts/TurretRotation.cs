using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public KeyCode toggleBalisticComputer = KeyCode.Q;
    public bool balisticComputer = false;

    public Vector2 cannonLimits;

    public float elevationSpeed = 12f;
    public float rotationSpeed = 30f;


    public float cannonRimRadius = 0f;
    public float cannonHeight = 0f;


    public float elevation;
    public float rotation;

    private float targetElevation;
    private float targetRotation;


    public Transform camera;
    public Transform cannon;
    public Transform hitIndicator;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleBalisticComputer)) balisticComputer = !balisticComputer;

        RaycastHit targettingRay;
        if (Physics.Raycast(camera.position + camera.forward * 15f, camera.forward, out targettingRay)) SetTarget(targettingRay.point);
        else SetTarget(camera.position + camera.forward * 1000f);

        //if (Mathf.Abs(elevation - targetElevation) < elevationSpeed * Time.deltaTime) elevation = targetElevation;
        //else if (elevation < targetElevation) elevation += elevationSpeed * Time.deltaTime;
        //else if (elevation > targetElevation) elevation -= elevationSpeed * Time.deltaTime;
        //elevation = Mathf.Clamp(elevation, -cannonLimits.y, -cannonLimits.x);

        elevation = Mathf.MoveTowardsAngle(elevation, targetElevation, elevationSpeed * Time.deltaTime);
        elevation = Mathf.Clamp(elevation, -cannonLimits.y, -cannonLimits.x);

        //if (rotation < -180f) rotation += 360f;
        //else if (rotation > 180f) rotation -= 360f;

        //float rotationDifference = Mathf.Abs(rotation - targetRotation);
        //if (rotationDifference > 180f) rotationDifference -= 360f;

        //if (Mathf.Abs(rotationDifference) < elevationSpeed * Time.deltaTime) rotation = targetRotation;
        //else if (rotationDifference > 0)
        //{
        //    //rotation -= rotationSpeed * Time.deltaTime;
        //    if(rotation < targetRotation) rotation += rotationSpeed * Time.deltaTime;
        //    else rotation -= rotationSpeed * Time.deltaTime;
        //}
        //else if (rotationDifference < 0)// rotation += rotationSpeed * Time.deltaTime;
        //{
        //    if (rotation < targetRotation) rotation -= rotationSpeed * Time.deltaTime;
        //    else rotation += rotationSpeed * Time.deltaTime;
        //}

        rotation = Mathf.MoveTowardsAngle(rotation, targetRotation, rotationSpeed * Time.deltaTime);


        //Debug.Log(targetRotation + "  /  " + rotation + "  //  " + rotationDifference);

        //if(rotation + 180 > targetRotation)
        //rotation += rotationSpeed * Time.deltaTime;

        transform.localEulerAngles = new Vector3(0, rotation, 0);
        cannon.localEulerAngles = new Vector3(elevation, 0, 0);
    }

    ///If the turret is aiming off remember to put turret script on object which is nested in object to which it is on position 0;0;0 couse script needs this
    public void SetTarget(Vector3 target)
    {
        float ballisticAngle = 0f;
        if (balisticComputer)
        {
            ballisticAngle = CalculateBallisticAngle(target);

            Debug.Log(ballisticAngle * Mathf.Rad2Deg);
            //target = //calculate from angle
        }

        Vector3 localTarget = transform.parent.InverseTransformPoint(target);

        float x = localTarget.x;
        float y = localTarget.y;
        float z = localTarget.z;

        targetRotation = -Vector2.SignedAngle(Vector2.right, new Vector2(x, z)) + 90f;

        if(balisticComputer) targetElevation = -ballisticAngle * Mathf.Rad2Deg;
        else targetElevation = -Vector2.SignedAngle(Vector2.right, new Vector2(new Vector2(x, z).magnitude - cannonRimRadius, y - cannonHeight));
    }

    public float CalculateBallisticAngle(Vector3 target)
    {
        //trza będzie obliczyć kąt między płaszczyzną wieżyczki a wektorem idącym w stronę celu (ale o y = turret.worldPosition.y)
        //hmm, można to zrobić biorąc turret.up i wektor ten płaski i mierząc kąt pomiędzy nimi ale to jest TODO

        float maxAngle = (Vector3.Angle(transform.up, ExtractXZ(target - transform.position)) - 90f + cannonLimits.y) * Mathf.Deg2Rad; //maksymalny kąt działa wynikający z nachylenia pojazdu (in rads)
        float minAngle = (Vector3.Angle(transform.up, ExtractXZ(target - transform.position)) - 90f + cannonLimits.x) * Mathf.Deg2Rad; ; //minimalny (...)

        float angle = maxAngle;


        float v = cannon.GetComponent<CannonUtility>().pelletVelocity;//Scrap it for optimalisation if needed
        float g = Physics.gravity.y;//Scrap it for optimalisation if needed

        float distance = Vector2.Distance(ExtractXZ(transform.position), ExtractXZ(target)) - cannonRimRadius; //(S.x - T.x) //Patrz tu w razie czego po błędy
        float heightDifference = transform.position.y + cannonHeight - target.y; //(S.y - T.y)


        //sqrt = sqrt(v^2(v^2-v-2g(S.y - T.y))^2 - 4v(-v+1)g^2(T.x-S.x)) //dla ++ i --
        float sqrt = Mathf.Sqrt(v*v*Mathf.Pow(v*v - v - 2*g*heightDifference, 2) - 4*v*(-v+1)*g*g*distance);
        float nominator = v*(v*v - v - 2*g*(heightDifference));
        float denominator = 2*v*v*(-v+1);

        float potentialAngle = Mathf.Acos((nominator + sqrt) / denominator);
        if (potentialAngle <= maxAngle && potentialAngle >= minAngle && potentialAngle < angle) angle = potentialAngle; //favours flatter angles

        potentialAngle = Mathf.Acos((nominator - sqrt) / denominator);
        if (potentialAngle <= maxAngle && potentialAngle >= minAngle && potentialAngle < angle) angle = potentialAngle;


        //dla +- i -+
        sqrt = Mathf.Sqrt(v * v * Mathf.Pow(v * v + v - 2 * g * -heightDifference, 2) - 4 * v * (-v - 1) * g * g * distance);
        nominator = v * (v * v + v - 2 * g * (-heightDifference));
        denominator = 2 * v * v * (-v - 1);

        potentialAngle = Mathf.Acos((nominator + sqrt) / denominator);
        if (potentialAngle <= maxAngle && potentialAngle >= minAngle && potentialAngle < angle) angle = potentialAngle; //favours flatter angles

        potentialAngle = Mathf.Acos((nominator - sqrt) / denominator);
        if (potentialAngle <= maxAngle && potentialAngle >= minAngle && potentialAngle < angle) angle = potentialAngle;

        return angle;
    }
    

    static Vector2 ExtractXZ(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
    //static float Pow(float a)
    //{
    //    return a * a;
    //}
}
