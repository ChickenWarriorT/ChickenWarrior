using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurve
{
    // Calculates the position along the Bezier curve
    public static Vector3 GetPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * control + t * t * end;
    }
}