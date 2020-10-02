using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float elevationSpeedMultiplier = 1f;
    public float ElevationSpeedMultiplier { get { return elevationSpeedMultiplier; } }

    public virtual void Fire()
    { }
}
