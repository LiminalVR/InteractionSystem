﻿using UnityEngine;

public static class AngleUtils
{
    public static bool WithinSight(Vector3 to, Vector3 from, Vector3 forward, float angle)
    {
        return GetAngle(to, from, forward) <= angle / 2;
    }

    public static float GetAngle(Vector3 to, Vector3 from, Vector3 forward)
    {
        Vector3 tarPosition = to;
        tarPosition.y = from.y;

        Vector3 targetDir = tarPosition - from;

        var angleToObject = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        angleToObject = Mathf.Abs(angleToObject);

        return angleToObject;
    }

    //Calculate Position
    public static Vector3 GetPosition(float angle, float dist, Vector3 root, Transform from)
    {
        var transformAngle = from.eulerAngles.y;

        var r = Mathf.Deg2Rad * (angle + 90 - transformAngle);
        var x = Mathf.Cos(r);
        var y = Mathf.Sin(r);

        Vector3 displacement = new Vector3(x, 0, y) * dist;
        Vector3 pos = root + displacement;
        return pos;
    }
}