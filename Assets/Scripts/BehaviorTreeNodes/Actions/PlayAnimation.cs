using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public class PlayAnimation : EnemyAction
    {


        public string animationTriggerName;
        public override void OnStart()
        {
            animator.SetTrigger(animationTriggerName);
        }
    }
}
