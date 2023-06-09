using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField]
    private MovementType movementType;

    private Vector2 direction;

    private float distanceDetectBoundary = 2.0f;

    private Vector2 normalVector;

    private void OnEnable()
    {
        direction = Utilities.RandomDirection();
        Debug.Log("�����������----------��" + direction);
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
    private void MovementChasing()
    {
        Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
        Vector2 position = transform.position;
        Vector2 moveDir = (playerPosition - position).normalized;

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, GetComponent<Enemy>().MoveSpeed * Time.fixedDeltaTime);
    }
    private void MovementReflect()
    {
        Vector2 position = transform.position;
        Vector2 moveDir = direction;

        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, GetComponent<Enemy>().MoveSpeed * Time.fixedDeltaTime);

        //����Ƿ��ڵ�ͼ�߽�
        if (Utilities.IsAtBoundary(transform, distanceDetectBoundary, out normalVector))
        {
            direction = Utilities.RefectDirection(direction, normalVector);
            Debug.Log("�������---------��" + direction);
        }
    }
}
public enum MovementType
{
    Chasing = 1,
    Reflect = 2
}

