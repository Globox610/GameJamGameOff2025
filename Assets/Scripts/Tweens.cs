using UnityEngine;

public class Tweens
{
    public  static Vector3 BezierLerp(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }
}
