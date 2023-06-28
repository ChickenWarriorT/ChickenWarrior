using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

public class BezierMover : MonoBehaviour
{

    //public Vector2 controlPoint1;
    //public Vector2 controlPoint2;
    //public Vector2 endPoint;
    //public float speed = 1f;

    //private Vector2 startPoint;
    //private List<Vector2> points;

    //void Start()
    //{
    //    startPoint = transform.position;
    //    points = GeneratePoints();
    //    StartCoroutine(MoveAlongCurve());
    //}

    //IEnumerator MoveAlongCurve()
    //{
    //    foreach (var point in points)
    //    {
    //        while ((Vector2)transform.position != point)
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);

    //            // Update rotation to face the direction of movement
    //            Vector2 direction = point - (Vector2)transform.position;
    //            if (direction != Vector2.zero)
    //            {
    //                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //                transform.rotation = Quaternion.Euler(0, 0, angle);
    //            }

    //            yield return null;
    //        }
    //    }
    //}

    //List<Vector2> GeneratePoints()
    //{
    //    List<Vector2> points = new List<Vector2>();
    //    int segments = 4;
    //    float step = 1f / segments;
    //    for (float t = 0; t <= 1; t += step)
    //    {
    //        Vector2 position = CalculateBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
    //        points.Add(position);
    //    }
    //    return points;
    //}

    //Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    //{
    //    float u = 1 - t;
    //    float tt = t * t;
    //    float uu = u * u;
    //    float uuu = uu * u;
    //    float ttt = tt * t;

    //    Vector2 p = uuu * p0; //term 1
    //    p += 3 * uu * t * p1; //term 2
    //    p += 3 * u * tt * p2; //term 3
    //    p += ttt * p3; //term 4

    //    return p;
    //}

    //void OnDrawGizmos()
    //{
    //    Vector2 startPoint = this.startPoint; //假设起点为物体当前的位置
    //    Vector2 controlPoint = this.controlPoint1; //假设控制点为(10,10,0)
    //    Vector2 controlPoint2 = this.controlPoint2; //假设第二个控制点为(20,10,0)
    //    Vector2 endPoint = this.endPoint; //假设终点为(20,0,0)


    //    Gizmos.color = Color.red;

    //    //绘制起点到控制点的线段
    //    Gizmos.DrawLine(startPoint, controlPoint1);

    //    //绘制控制点到控制点的线段
    //    Gizmos.DrawLine(controlPoint1, controlPoint2);

    //    //绘制控制点到终点的线段
    //    Gizmos.DrawLine(controlPoint2, endPoint);

    //    Gizmos.color = Color.white;

    //    //绘制贝塞尔曲线
    //    Vector3 lastPos = startPoint;
    //    for (float t = 0; t <= 1; t += 0.01f)
    //    {
    //        Vector2 currentPos = Mathf.Pow(1 - t, 3) * startPoint +
    //                            3 * Mathf.Pow(1 - t, 2) * t * controlPoint1 +
    //                            3 * (1 - t) * t * t * controlPoint2 +
    //                            t * t * t * endPoint;
    //        Gizmos.DrawLine(lastPos, currentPos);
    //        lastPos = currentPos;
    //    }
    //}
    public Vector2 point1;
    public Vector2 point2;
    public Vector2 point3;
    public Vector2 point4;
    public float duration;

    private Vector2 startPoint;

    void Start()
    {
        startPoint = transform.position;
        StartCoroutine(MoveAlongCurve());
    }

    IEnumerator MoveAlongCurve()
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector2 position = CalculateCatmullRomPoint(t, point1, point2, point3, point4);
            transform.position = position;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = point3; // Ensure the endpoint is reached
    }

    Vector2 CalculateCatmullRomPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Vector2 a = 2f * p1;
        Vector2 b = p2 - p0;
        Vector2 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector2 d = -p0 + 3f * p1 - 3f * p2 + p3;

        Vector2 p = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return p;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (float t = 0; t <= 1; t += 0.01f)
        {
            Gizmos.DrawSphere(CalculateCatmullRomPoint(t, point1, point2, point3, point4), 0.1f);
        }
    }
}



