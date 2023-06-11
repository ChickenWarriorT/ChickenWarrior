using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class BehaviorTreeNode : Action
{
    public SharedInt testInt;

    public override TaskStatus OnUpdate()
    {
        testInt = 2;
        return TaskStatus.Success;
    }
}
