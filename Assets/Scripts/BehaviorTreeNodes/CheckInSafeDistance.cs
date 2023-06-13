using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckInSafeDistance : Conditional
{
    private float safeDistance;
    public override void OnAwake()
    {
        safeDistance = GetComponent<Monster>().SafeDistance;
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(transform.position, PlayerManager._instance.player.transform.position) < safeDistance)
        {
            
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
    public override void OnFixedUpdate()
    {
       
    }

  
}
