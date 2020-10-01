using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public HeadRotation head;
    public Vector2 range_;
    Range range;
    public float elevationSpeed = 120f;

    void Awake()
    {
        range = new Range(range_);
    }

    float elevation;
    void Update()
    {
        elevation = Mathf.MoveTowards(elevation, range.Clamp(head.CameraV), elevationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(elevation, 0f, 0f);
    }
}
