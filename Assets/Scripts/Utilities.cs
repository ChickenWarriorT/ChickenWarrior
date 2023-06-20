using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //�������
    public static Vector2 RandomDirection()
    {
        return Random.insideUnitCircle * 1.0f;
    }

    //���䷽��,�����ƶ�����direction���ͷ�����normal����
    public static Vector2 RefectDirection(Vector2 direction, Vector2 normal)
    {
        return Vector2.Reflect(direction, normal);
    }

    //���߼���Ƿ������߼죬distanceDetectBoundaryΪ���߳��ȣ�normalVector Ϊ��ײ��ķ�����
    public static bool IsAtBoundary(Transform transform, float distanceDetectBoundary, out Vector2 normalVector)
    {
        Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in directions)
        {
            Ray2D ray = new Ray2D(transform.position, dir);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceDetectBoundary, 1 << LayerMask.NameToLayer("Boundary"));
            //���߼�⵽����
            if (hit.collider != null)
            {

                // �����ײ�Ƿ�Ϊtrigger
                if (hit.collider.isTrigger)
                {
                    //������ײ��ķ�����
                    normalVector = hit.normal;
                    Debug.Log("The object is a trigger");
                    return true;
                }
            }
            // Draw a debug ray in the scene view to show where the raycast is hitting
            Debug.DrawRay(ray.origin, ray.direction * distanceDetectBoundary, Color.green);
        }
        normalVector = Vector2.zero;
        return false;
    }

    public static Vector2 DirectionFromAToB(Transform from,Transform to)
    {
        var direction = (to.position - from.position).normalized;
        return direction;
    }
}
