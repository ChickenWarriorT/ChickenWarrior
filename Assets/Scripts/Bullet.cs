using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Transform currentTarget;

    /// <summary>
    /// 子弹的的前进方向
    /// </summary>
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

    [SerializeField]
    private Vector2 startPosition;

    [SerializeField]
    private float afterbounceDistance = 5;

    [SerializeField]
    private float flyDistance;

    private float flyTime;

    private float currentFlyDistance;

    private GameObject caster;

    [SerializeField]
    private bool isTracing;

    private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //Destroy(this.gameObject, 10);
        flyTime = ConvertFlyDistanceToTime(flyDistance,moveSpeed);
    }

    private void FixedUpdate()
    {
        Move();
    }


    //子弹移动
    private void Move()
    {
        DestoryOnTime();
        //fixedupdate每秒50帧，所以乘以50
        rb.velocity = dir * moveSpeed*50*Time.fixedDeltaTime;

        //如果是追踪子弹
        if (isTracing)
        {
            ChangeDirection(target);

        }
    }

    //改变子弹飞行轨迹，根据目标单位
    public void ChangeDirection(Transform target)
    {
        if (target != null)
        {
            dir = (target.position - transform.position).normalized;
            
            //改变子弹朝向，目标
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
    }

    //初始化
    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        this.target = target.transform;
        //currentTarget = target.transform;

        startPosition = this.transform.position;

        ChangeDirection(this.target);
        flyTime = ConvertFlyDistanceToTime(flyDistance, rb.velocity.magnitude);
        Debug.Log("飞行时间:" + flyDistance + ";" + flyTime);
        isMove = true;
    }


    //除当前目标外，寻找全体随机怪物
    private Enemy FindRandomEnemyNotIncludeTarget(Enemy target)
    {
        Debug.Log("怪物数量外部------：" + EnemyManager._instance.enemies.Count);
        //TODO 确认怪物生成后是否加入了List 怪物死亡是否移除
        List<Enemy> enemyList = new(EnemyManager._instance.enemies);
        Debug.Log("怪物数量外部：" + EnemyManager._instance.enemies.Count);

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
        if (flyTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    //飞行距离，根据速度转换成时间
    public float ConvertFlyDistanceToTime(float distance,float speed)
    {
        return distance / speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Character character = other.GetComponent<Character>();
            character.TakeDamage(damage);
            if (bounceCount > 0)
            {
                //GetComponent<Collider2D>().enabled = false;
                bounceCount--;
                var tempEnemy = FindRandomEnemyNotIncludeTarget((Enemy)character);

                if (tempEnemy != null)
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
                    Debug.Log("随机飞行方向:"+dir);
                    target = null;
                    //修改飞行时间为最大飞行时间
                    flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                }
            }
            else
            {
                print("弹跳次数不足，销毁子弹。");
                Destroy(gameObject);
            }
        }
    }

}
