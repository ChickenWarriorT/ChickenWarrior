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

    public override void OnAwake()
    {
        Init();
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }
    public override void OnFixedUpdate()
    {
        Move();
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

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.fixedDeltaTime);
    }
    private void MovementReflect()
    {
        Vector2 position = transform.position;
        Vector2 moveDir = direction;

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
}
