using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckInSafeDistance : Conditional
{
    private float safeDistance;
    private float awayDistance;
    private float offset = 1f;
    private bool wasPlayerTooCloseLastFrame = false;
    public override void OnAwake()
    {
        safeDistance = GetComponent<Monster>().SafeDistance;
        awayDistance = safeDistance - offset;
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        var distance = Vector2.Distance(transform.position, PlayerManager._instance.player.transform.position);
        if (distance < awayDistance)
        {
            wasPlayerTooCloseLastFrame = true;
            return TaskStatus.Success;
        }
        else if (distance > safeDistance)
        {
            wasPlayerTooCloseLastFrame = false;
            return TaskStatus.Failure;
        }
        else
        {
            if (wasPlayerTooCloseLastFrame)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
    public override void OnFixedUpdate()
    {

    }


}
