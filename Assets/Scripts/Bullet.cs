using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Bullet : MonoBehaviour
{
    // ��ǰĿ��
    private Transform currentTarget;


    // �ӵ�����
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

    // �趨�ӵ����ٶȺͰ뾶�����ٶ�
    public float speed = 5f;
    public float radiusIncreaseRate = 1f;

    // ��¼�ӵ��ĽǶȺͰ뾶
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
    //        // ÿһ֡�����ӵ���λ��
    //        while (true)
    //        {
    //            // �����µİ뾶�ͽǶ�
    //            radius += radiusIncreaseRate * Time.deltaTime;
    //            angle += speed * Time.deltaTime;

    //            // �����µ�λ��
    //            Vector3 newPos = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);

    //            // �����ӵ���λ��
    //            transform.position = newPos;

    //            yield return null;
    //        }
    //    }
    

    //�ӵ��ƶ�
    private void Move()
    {
        DestoryOnTime();
        //fixedupdateÿ��50֡�����Գ���50
        rb.velocity = dir * moveSpeed * 50 * Time.fixedDeltaTime;
        //RayTracingDetect();
        //�����׷���ӵ�
        if (isTracing)
        {
            ChangeDirection(target.GetChild(0));

        }
    }

    //�ı��ӵ����й켣������Ŀ�굥λ
    public void ChangeDirection(Transform target)
    {
        if (IsNotNullOrInActive(target))
        {
            dir = (target.position - transform.position).normalized;

            //�ı��ӵ�����Ŀ��
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
        else
        {
            dir = transform.rotation * Vector2.up;
        }
    }
    //���target �ǿ� ���߲��
    private bool IsNotNullOrInActive(Transform target)
    {
        if (target != null && target.gameObject.activeSelf == true)
        {
            return true;
        }
        return false;
    }

    //��ʼ��
    public void Init(Vector2 startPos, GameObject target, int damage)
    {
        this.damage = damage;
        //�õ�hitpoint
        this.target = target.transform;
        //currentTarget = target.transform;

        transform.position = startPos;
        leftBounce = bounceCount;
        ChangeDirection(this.target.GetChild(0));
        flyMaxTimer = flyTimeMax;
        flyTime = ConvertFlyDistanceToTime(flyDistance, rb.velocity.magnitude);
        Debug.Log("����ʱ��:" + flyDistance + ";" + flyTime);
        isMove = true;
    }


    //����ǰĿ���⣬Ѱ��ȫ���������
    private Monster FindRandomEnemyNotIncludeTarget(Monster target)
    {
        Debug.Log("���������ⲿ------��" + MonsterManager._instance.monsters.Count);
        //TODO ȷ�Ϲ������ɺ��Ƿ������List ���������Ƿ��Ƴ�
        List<Monster> enemyList = new List<Monster>(MonsterManager._instance.monsters);
        Debug.Log("���������ⲿ��" + MonsterManager._instance.monsters.Count);

        enemyList.Remove(target);

        Debug.Log("���������ڲ���" + enemyList.Count);
        if (enemyList.Count <= 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, enemyList.Count);
        Debug.Log(randomIndex);
        return enemyList[randomIndex];
    }

    //�������
    public Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    //����������ʱ��ʱ������
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

    //���о��룬�����ٶ�ת����ʱ��
    public float ConvertFlyDistanceToTime(float distance, float speed)
    {
        return distance / speed;
    }

    //���߼��
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
                        //�õ����һ��enemy
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
                            print("�����������0ʱ��δ�ҵ���������������");
                            dir = GetRandomDirection();
                            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                            Debug.Log("������з���:" + dir);
                            target = null;
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }
                    }
                    else
                    {
                        print("�����������㣬�����ӵ���");
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
                            //�޸ķ���ʱ��Ϊ������ʱ��
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }
                        else
                        {
                            print("�����������0ʱ��δ�ҵ���������������");
                            dir = GetRandomDirection();
                            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                            Debug.Log("������з���:" + dir);
                            target = null;
                            //�޸ķ���ʱ��Ϊ������ʱ��
                            flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                        }


                    }
                    else
                    {
                        print("�����������㣬�����ӵ���");
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
