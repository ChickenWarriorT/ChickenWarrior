using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public class MonsterAction : Action
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected Player player;

        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = PlayerManager._instance.Player;
        }
    }
}