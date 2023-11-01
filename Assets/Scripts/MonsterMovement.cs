using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement
{
    private MovementType movementType;

    private Vector2 direction;

    private float distanceDetectBoundary = 2.0f;

    private Vector2 normalVector;

    private Transform monsterTransform;

    public MonsterMovement(MovementType type, Transform transform)
    {
        Init(type, transform);
    }


    public void Init(MovementType type, Transform transform)
    {
        movementType = type;
        direction = Utilities.RandomDirection();
        monsterTransform = transform;
        Debug.Log("出生随机方向----------：" + direction);
    }

    public void Move()
    {
        switch (movementType)
        {
            case MovementType.ChasingPlayer:
                MovementChasing();
                break;
            case MovementType.AwayFromPlayer:
                MovementReflect();
                break;
            case MovementType.ReflectWithBoundary:
                MovementReflect();
                break;
        }
    }
    private void MovementChasing()
    {
        Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
        Vector2 position = monsterTransform.position;
        Vector2 moveDir = (playerPosition - position).normalized;

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            monsterTransform.position = Vector2.MoveTowards(position, targetPosition, monsterTransform.GetComponent<Monster>().MoveSpeed * Time.fixedDeltaTime);
    }
    private void MovementReflect()
    {
        Vector2 position = monsterTransform.position;
        Vector2 moveDir = direction;

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            monsterTransform.position = Vector2.MoveTowards(position, targetPosition, monsterTransform.GetComponent<Monster>().MoveSpeed * Time.fixedDeltaTime);

        //检测是否在地图边界
        if (Utilities.IsAtBoundary(monsterTransform, distanceDetectBoundary, out normalVector))
        {
            direction = Utilities.RefectDirection(direction, normalVector);
            Debug.Log("随机方向---------：" + direction);
        }
    }
}
public enum MovementType
{
    ChasingPlayer = 1,
    ReflectWithBoundary = 2,
    AwayFromPlayer = 3,

}

