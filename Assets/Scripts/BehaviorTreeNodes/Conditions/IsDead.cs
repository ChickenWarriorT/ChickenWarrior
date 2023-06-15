using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsDead : Conditional
{
    private Character character;

    public override void OnAwake()
    {
        character = GetComponent<Character>();
    }

    public override TaskStatus OnUpdate()
    {
        if (character.CurrentHealth <= 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}
