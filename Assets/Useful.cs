using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Range
{
    float min;
    public float Min { get { return min; } }
    float max;
    public float Max { get { return max; } }


    public Range(float min, float max) { this.min = min; this.max = max; }
    public Range(Vector2 range) { min = range.x; max = range.y; }

    /// <summary>
    /// Clamps value to range specified by min and max.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Clamped value.</returns>
    public float Clamp(float value)
    {
        return Mathf.Clamp(value, min, max);
    }
}
