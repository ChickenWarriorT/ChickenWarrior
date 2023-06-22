using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class MonsterMove : Action
{
    public MovementType movementType;
    private float distanceDetectBoundary = 2.0f;
    private Vector2 direction;
    private Vector2 normalVector;
    private bool fixedUpdating = true;
    public override void OnAwake()
    {
        Init();
        base.OnAwake();
    }
    public override void OnFixedUpdate()
    {
        //Move();
        //fixedUpdating = false;
    }
    public override TaskStatus OnUpdate()
    {
        //if (!fixedUpdating)
        //{
        //    fixedUpdating = true;
        //    return TaskStatus.Success;
        //}
        //else
        //    return TaskStatus.Running;
        Move();
        return TaskStatus.Success;
    }




    public void Move()
    {
        switch (movementType)
        {
            case MovementType.Chasing:
                MovementChasing();
                break;
            case MovementType.Reflect:
                MovementReflect();
                break;
        }
    }

    public void Init()
    {
        direction = Utilities.RandomDirection();
        Debug.Log("出生随机方向----------：" + direction);
    }
    private void MovementChasing()
    {
        Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
        Vector2 position = transform.position;
        Vector2 moveDir = (playerPosition - position).normalized;
        FlipBasedOnDirection(moveDir.x);
        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.fixedDeltaTime);
    }
    private void MovementReflect()
    {
        Vector2 position = transform.position;
        Vector2 moveDir = direction;
        FlipBasedOnDirection(moveDir.x);
        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.fixedDeltaTime);

        //检测是否在地图边界
        if (Utilities.IsAtBoundary(transform, distanceDetectBoundary, out normalVector))
        {
            direction = Utilities.RefectDirection(direction, normalVector);
            Debug.Log("随机方向---------：" + direction);
        }
    }
    private void FlipBasedOnDirection(float directionX)
    {
        if (directionX > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
