using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsSkillPerforming : Conditional
{
    private bool isSkillPerforming;
    private Character character;

    public override void OnAwake()
    {
        character = GetComponent<Character>();
    }
    public override TaskStatus OnUpdate()
    {

        return character.IsSkillPerforming ? TaskStatus.Success : TaskStatus.Failure;
    }
}
