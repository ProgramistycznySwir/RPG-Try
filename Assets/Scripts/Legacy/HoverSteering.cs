using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSteering : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public Rigidbody rigidbody;
    public Transform hoversGroup;


    public float forwardPower = 100_000f;


    public float maxHoverPower = 30_000f;    

    public float maxHoverHeight = 5f;

    public int multiplier = 2;

    private Transform[] hovers;

    // Start is called before the first frame update
    void Start()
    {
        hovers = hoversGroup.GetComponentsInChildren<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        Hover();
    }

    void Hover()
    {
        foreach(Transform hover in hovers)
        {
            //float distance;

            Debug.Log(System.Convert.ToString(LayerMasker.layerOfHover, 2));

            RaycastHit raycast;

            rigidbody.AddForceAtPosition(hover.up * Time.deltaTime, hover.position);

            if (Physics.Raycast(hover.position, -hover.up, out raycast, maxHoverHeight, LayerMasker.layerOfHover))
            {
                float distance = Vector3.Distance(hover.position, raycast.point);

                Debug.Log("Doing my part!");

                rigidbody.AddForceAtPosition(hover.up / Mathf.Pow(distance, multiplier) * Time.deltaTime * maxHoverPower, hover.position);
                //Debug.Log((hover.up / Mathf.Pow(distance, multiplier) * Time.deltaTime).magnitude);
            }
        }
    }
}
