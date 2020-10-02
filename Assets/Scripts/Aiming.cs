using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Transform[] elevated;
    public Transform[] rotated;

    public HeadRotation head;
    public Vector2 range_;
    Range range;
    float elevation;
    public float elevationSpeed = 120f;

    public Weapon weapon;

    void Awake()
    {
        range = new Range(range_);
        elevation = range.Max;
    }

    void Update()
    {
        elevation = Mathf.MoveTowards(elevation, range.Clamp(head.CameraV), elevationSpeed * weapon.elevationSpeedMultiplier * Time.deltaTime);
        foreach(Transform transform in elevated)
            transform.localEulerAngles = new Vector3(elevation, 0f, 0f);
    }
}
