using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Bullet : MonoBehaviour
{
    // 当前目标
    private Transform currentTarget;


    // 子弹方向
    private Vector2 dir;

    private bool isMove = false;

    private Vector2 _moveDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int damage;

    [SerializeField]
    private int bounceCount;
    private int leftBounce;

    [SerializeField]
    private Vector2 startPosition;

    [SerializeField]
    private float afterbounceDistance = 5;

    [SerializeField]
    private float flyDistance;

    private float flyTime;
    private float flyTimeMax = 20.0f;
    private float flyMaxTimer;

    private float currentFlyDistance;

    [SerializeField]
    private bool isTracing;

    private Transform target;
    [SerializeField]
    private float detectDistance = 0.01f;

    // 设定子弹的速度和半径增长速度
    public float speed = 5f;
    public float radiusIncreaseRate = 1f;

    // 记录子弹的角度和半径
    private float angle = 0f;
    private float radius = 0f;

    private System.Action<Bullet> deactiveBullet;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flyTime = ConvertFlyDistanceToTime(flyDistance, moveSpeed);
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void OnEnable()
    {
    }



    //public void StartMoving()
    //    {
    //        StartCoroutine(MoveInCircle());
    //    }

    //    IEnumerator MoveInCircle()
    //    {
    //        // 每一帧更新子弹的位置
    //        while (true)
    //        {
    //            // 计算新的半径和角度
    //            radius += radiusIncreaseRate * Time.deltaTime;
    //            angle += speed * Time.deltaTime;

    //            // 计算新的位置
    //            Vector3 newPos = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);

    //            // 更新子弹的位置
    //            transform.position = newPos;

    //            yield return null;
    //        }
    //    }
    

    //子弹移动
    private void Move()
    {
        DestoryOnTime();
        //fixedupdate每秒50帧，所以乘以50
        rb.velocity = dir * moveSpeed * 50 * Time.fixedDeltaTime;
        //RayTracingDetect();
        //如果是追踪子弹
        if (isTracing)
        {
            ChangeDirection(target.GetChild(0));

        }
    }

    //改变子弹飞行轨迹，根据目标单位
    public void ChangeDirection(Transform target)
    {
        if (IsNotNullOrInActive(target))
        {
            dir = (target.position - transform.position).normalized;

            //改变子弹朝向，目标
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
        else
        {
            dir = transform.rotation * Vector2.up;
        }
    }
    //检测target 是空 或者不活动
    private bool IsNotNullOrInActive(Transform target)
    {
        if (target != null && target.gameObject.activeSelf == true)
        {
            return true;
        }
        return false;
    }

    //初始化
    public void Init(Vector2 startPos, GameObject target, int damage)
    {
        this.damage = damage;
        //拿到hitpoint
        this.target = target.transform;
        //currentTarget = target.transform;

        transform.position = startPos;
        leftBounce = bounceCount;
        ChangeDirection(this.target.GetChild(0));
        flyMaxTimer = flyTimeMax;
        flyTime = ConvertFlyDistanceToTime(flyDistance, rb.velocity.magnitude);
        Debug.Log("飞行时间:" + flyDistance + ";" + flyTime);
        isMove = true;
    }


    //除当前目标外，寻找全体随机怪物
    private Monster FindRandomEnemyNotIncludeTarget(Monster target)
    {
        Debug.Log("怪物数量外部------：" + MonsterManager._instance.monsters.Count);
        //TODO 确认怪物生成后是否加入了List 怪物死亡是否移除
        List<Monster> enemyList = new List<Monster>(MonsterManager._instance.monsters);
        Debug.Log("怪物数量外部：" + MonsterManager._instance.monsters.Count);

        enemyList.Remove(target);

        Debug.Log("怪物数量内部：" + enemyList.Count);
        if (enemyList.Count <= 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, enemyList.Count);
        Debug.Log(randomIndex);
        return enemyList[randomIndex];
    }

    //随机方向
    public Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    //到达最大飞行时间时，销毁
    private void DestoryOnTime()
    {
        flyTime -= Time.fixedDeltaTime;
        flyMaxTimer -= Time.fixedDeltaTime;
        if (flyTime <= 0.0f)
        {
            deactiveBullet.Invoke(this);
        }
        if (flyMaxTimer <= 0.0f)
        {
            deactiveBullet.Invoke(this);
        }
    }

    //飞行距离，根据速度转换成时间
    public float ConvertFlyDistanceToTime(float distance, float speed)
    {
        return distance / speed;
    }

    //射线检测
    private void RayTracingDetect()
    {
        Ray2D ray = new Ray2D(transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, detectDistance);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Character character = hit.collider.GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(damage);
                if (leftBounce > 0)
                {
                    leftBounce--;
                    Monster monster = character as Monster;
                    if (monster != null)
                    {
                        //拿到随机一个enemy
                        var tempEnemy = FindRandomEnemyNotIncludeTarget(monster);

                        if (tempEnemy != null)
                        {
                            target = tempEnemy.transform ;
                            print(tempEnemy.name);
                            ChangeDirection(target);
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }
                        else
                        {
                            print("弹射次数大于0时，未找到怪物，向随机方向发射");
                            dir = GetRandomDirection();
                            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                            Debug.Log("随机飞行方向:" + dir);
                            target = null;
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }
                    }
                    else
                    {
                        print("弹跳次数不足，销毁子弹。");
                        deactiveBullet.Invoke(this);
                    }
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction , Color.green);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(damage);
                if (leftBounce > 0)
                {
                    leftBounce--;
                    Monster monster = character as Monster;
                    if (monster != null)
                    {
                        var tempEnemy = FindRandomEnemyNotIncludeTarget(monster);

                        if (tempEnemy != null)//&& tempEnemy.gameObject.activeSelf == true
                        {
                            target = tempEnemy.transform;
                            print(tempEnemy.name);
                            ChangeDirection(target);
                            //修改飞行时间为最大飞行时间
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }
                        else
                        {
                            print("弹射次数大于0时，未找到怪物，向随机方向发射");
                            dir = GetRandomDirection();
                            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                            Debug.Log("随机飞行方向:" + dir);
                            target = null;
                            //修改飞行时间为最大飞行时间
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }


                    }
                    else
                    {
                        print("弹跳次数不足，销毁子弹。");
                        deactiveBullet.Invoke(this);
                    }
                }
            }
        }
    }
    public void SetDeactiveBullet(System.Action<Bullet> deactiveAction)
    {
        deactiveBullet = deactiveAction;
    }
}
