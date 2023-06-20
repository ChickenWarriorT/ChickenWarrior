using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttack : Conditional
{
    private Character character;

    public override void OnAwake()
    {
        character = GetComponent<Character>();
    }

    public override TaskStatus OnUpdate()
    {
        if (character.CanAttack(PlayerManager._instance.Player.transform))
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}
