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

public class My
{
    public class Vector2
    {
        public static Vector2 ElipticNormalization(UnityEngine.Vector2 vector)
        {
            // 2 podejścia:
            // 1. Stworzenie elipsy z 2 współrzędnych wektora i znalezienia punktu przecięcia się elipsy i wektora.
            // 2. Znalezienie prostokąta o największym polu w obrębie elipsy (o jednym punkcie w [0,0]). (może być trudniejsze)

            throw new System.NotImplementedException();
        }
    }
}
