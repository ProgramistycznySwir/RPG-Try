using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Aiming aiming;

    public float elevationSpeedMultiplier_ = 1f;
    float __elevationSpeedMultiplier = 1f;
    public float elevationSpeedMultiplier { get { return elevationSpeedMultiplier; } }

    public virtual void Fire()
    { }
}
