using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class CheckAwaySafeDistance : Conditional
{
    private float awayDistance;
    private float offSet = 1f;
    public override void OnAwake()
    {
        awayDistance = GetComponent<Monster>().SafeDistance - offSet;
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        float distance = Vector2.Distance(transform.position, PlayerManager._instance.player.transform.position);
        
        if ( distance < awayDistance ||distance < GetComponent<Monster>().SafeDistance)
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
