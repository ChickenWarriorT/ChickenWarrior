using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public enum DistanceCheckType
    {
        GreaterThanSafeDistance,
        LessThanAwayDistance,
        GreaterThanAwayLessThanSafe
    }
    public class DistanceChecking : Conditional
    {
        private float safeDistance;
        private float awayDistance;

        public DistanceCheckType checkType;

        public override void OnAwake()
        {
            safeDistance = GetComponent<Monster>().SafeDistance;
            awayDistance = GetComponent<Monster>().AwayDistance;
            base.OnAwake();
        }
        public override TaskStatus OnUpdate()
        {
            switch (checkType)
            {
                case DistanceCheckType.GreaterThanSafeDistance:
                    return GreaterThanSafeDistance();
                case DistanceCheckType.LessThanAwayDistance:
                    return LessThanAwayDistance();
                case DistanceCheckType.GreaterThanAwayLessThanSafe:
                    return GreaterThanAwayLessThanSafe();
                default:
                    return TaskStatus.Failure;
            }

        }

        public TaskStatus GreaterThanSafeDistance()
        {
            var distance = Vector2.Distance(transform.position, PlayerManager._instance.Player.transform.position);
            if (distance >= safeDistance)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
        public TaskStatus LessThanAwayDistance()
        {
            var distance = Vector2.Distance(transform.position, PlayerManager._instance.Player.transform.position);
            if (distance <= awayDistance)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
        public TaskStatus GreaterThanAwayLessThanSafe()
        {
            var distance = Vector2.Distance(transform.position, PlayerManager._instance.Player.transform.position);
            if (distance >= awayDistance && distance <= safeDistance)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

}
