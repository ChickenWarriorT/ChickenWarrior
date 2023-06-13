using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveAway : Action
{
    public override TaskStatus OnUpdate()
    {
        MoveAwayFromPlayer();
        return base.OnUpdate();
    }

    public void MoveAwayFromPlayer()
    {
        Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
        Vector2 position = transform.position;
        Vector2 moveDir = (position - playerPosition).normalized;
        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);
        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.deltaTime);
    }

}
