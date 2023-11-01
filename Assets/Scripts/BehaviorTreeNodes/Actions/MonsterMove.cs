using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public enum MovementType
    {
        ChasingPlayer,
        AwayFromPlayer,
        ReflectWithBoundary
    }
    public class MonsterMove : Action
    {
        public MovementType movementType;
        private float distanceDetectBoundary = 2.0f;
        private Vector2 direction;
        private Vector2 normalVector;
        public override void OnAwake()
        {
            Init();
            base.OnAwake();
        }
 
        public override TaskStatus OnUpdate()
        {
            Move();
            return TaskStatus.Running;
        }

        public void Move()
        {
            switch (movementType)
            {
                case MovementType.ChasingPlayer:
                    MoveChasingPlayer();
                    break;
                case MovementType.AwayFromPlayer:
                    MoveAwayFromPlayer();
                    break;
                case MovementType.ReflectWithBoundary:
                    MoveReflectWithBoundary();
                    break;
            }
        }

        //初始化移动行为
        public void Init()
        {
            direction = Utilities.RandomDirection();
            Debug.Log("出生随机方向----------：" + direction);
        }
        private void MoveChasingPlayer()
        {
            Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
            Vector2 position = transform.position;
            Vector2 moveDir = (playerPosition - position).normalized;
            FlipBasedOnDirection(moveDir.x);
            Vector2 targetPosition = position + moveDir;

            targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

            if (position != targetPosition)
                transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.deltaTime);
        }

        private void MoveAwayFromPlayer()
        {
            Vector2 playerPosition = PlayerManager._instance.PlayerPosition;
            Vector2 position = transform.position;
            Vector2 moveDir = (position - playerPosition).normalized;
            Vector2 targetPosition = position + moveDir;

            targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);
            if (position != targetPosition)
                transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.deltaTime);
        }

        private void MoveReflectWithBoundary()
        {
            Vector2 position = transform.position;
            Vector2 moveDir = direction;
            FlipBasedOnDirection(moveDir.x);
            Vector2 targetPosition = position + moveDir;

            targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

            if (position != targetPosition)
                transform.position = Vector2.MoveTowards(position, targetPosition, transform.GetComponent<Monster>().MoveSpeed * Time.deltaTime);

            //检测是否在地图边界
            if (Utilities.IsAtBoundary(transform, distanceDetectBoundary, out normalVector))
            {
                direction = Utilities.RefectDirection(direction, normalVector);
                Debug.Log("随机方向---------：" + direction);
            }
        }
        private void FlipBasedOnDirection(float directionX)
        {
            if (directionX > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (directionX < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}

