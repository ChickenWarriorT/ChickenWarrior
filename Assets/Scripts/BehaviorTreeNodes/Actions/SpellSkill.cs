using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SpellSkill : Action
{
    private Character character;

    public override void OnAwake()
    {
        character = GetComponent<Character>();

    }
    public override TaskStatus OnUpdate()
    {

        character.Skill();

        return TaskStatus.Success;
    }



}
