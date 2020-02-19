using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CustomCenterOfMass : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Vector3 centerOfMass;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Old center of mass of " + transform.name + " : " + rigidbody.centerOfMass);
        rigidbody.centerOfMass = centerOfMass;
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
