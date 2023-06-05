using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Vector2 direction;
    
    private float distanceDetectBoundary = 2.0f;

    private Vector2 normalVector;

    private void OnEnable()
    {
        direction = RandomDirection();
        Debug.Log("出生随机方向----------：" + direction);
    }

    private void FixedUpdate()
    {
        Move();
    }

    //随机方向
    private Vector2 RandomDirection()
    {
        return Random.insideUnitCircle * 1.0f;
    }
    
    //反射方向
    private Vector2 RefectDirection(Vector2 normal)
    {
        return Vector2.Reflect(direction, normal);
    }

    //private bool IsAtBoundary()
    //{
    //    Ray2D ray = new Ray2D(transform.position, direction);

    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceDetectBoundary,1<<LayerMask.NameToLayer("Boundary"));
    //    //射线检测到东西
    //    if (hit.collider != null)
    //    {

    //        // Check if the object is a trigger
    //        if (hit.collider.isTrigger)
    //        {
    //            Debug.Log("The object is a trigger");
    //            return true;
    //        }
    //    }
    //    // Draw a debug ray in the scene view to show where the raycast is hitting
    //    Debug.DrawRay(ray.origin, ray.direction * distanceDetectBoundary, Color.green);
    //    return false;
    //}

    //是否碰到
    private bool IsAtBoundary()
    {
        Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in directions)
        {

            Ray2D ray = new Ray2D(transform.position, dir);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distanceDetectBoundary, 1 << LayerMask.NameToLayer("Boundary"));
            //射线检测到东西
            if (hit.collider != null)
            {

                // Check if the object is a trigger
                if (hit.collider.isTrigger)
                {
                    normalVector = hit.normal;
                    Debug.Log("The object is a trigger");
                    return true;
                }
            }
            // Draw a debug ray in the scene view to show where the raycast is hitting
            Debug.DrawRay(ray.origin, ray.direction * distanceDetectBoundary, Color.green);
        }
        return false;
    }

    private void Move()
    {
        Vector2 position = transform.position;
        Vector2 moveDir = direction;

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, GetComponent<Enemy>().MoveSpeed * Time.fixedDeltaTime);

        if (IsAtBoundary())
        {
            //direction = RandomDirection();
            direction = RefectDirection(normalVector);
            Debug.Log("随机方向---------：" + direction);
        }
    }

}
