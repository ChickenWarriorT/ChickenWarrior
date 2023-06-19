using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public enum AnimationType
{
    Die,
    Walk,
    Skill_01,
    Skill_02
}
public class PlayAnimation : Action
{
    private Animator animator;
    public AnimationType animationType;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        switch (animationType)
        {
            case (AnimationType.Die):
                PlayDie();
                return TaskStatus.Success;
            default:
                return TaskStatus.Failure;

        }
    }

    public void PlayDie()
    {
        animator.SetTrigger("Die");
        
    }



}
