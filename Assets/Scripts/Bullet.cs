using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Transform currentTarget;

    /// <summary>
    /// �ӵ��ĵ�ǰ������
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


    //�ӵ��ƶ�
    private void Move()
    {
        DestoryOnTime();
        //fixedupdateÿ��50֡�����Գ���50
        rb.velocity = dir * moveSpeed*50*Time.fixedDeltaTime;

        //�����׷���ӵ�
        if (isTracing)
        {
            ChangeDirection(target);

        }
    }

    //�ı��ӵ����й켣������Ŀ�굥λ
    public void ChangeDirection(Transform target)
    {
        if (target != null)
        {
            dir = (target.position - transform.position).normalized;
            
            //�ı��ӵ�����Ŀ��
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        }
    }

    //��ʼ��
    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        this.target = target.transform;
        //currentTarget = target.transform;

        startPosition = this.transform.position;

        ChangeDirection(this.target);
        flyTime = ConvertFlyDistanceToTime(flyDistance, rb.velocity.magnitude);
        Debug.Log("����ʱ��:" + flyDistance + ";" + flyTime);
        isMove = true;
    }


    //����ǰĿ���⣬Ѱ��ȫ���������
    private Enemy FindRandomEnemyNotIncludeTarget(Enemy target)
    {
        Debug.Log("���������ⲿ------��" + EnemyManager._instance.enemies.Count);
        //TODO ȷ�Ϲ������ɺ��Ƿ������List ���������Ƿ��Ƴ�
        List<Enemy> enemyList = new(EnemyManager._instance.enemies);
        Debug.Log("���������ⲿ��" + EnemyManager._instance.enemies.Count);

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
        if (flyTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    //���о��룬�����ٶ�ת����ʱ��
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
                    //�޸ķ���ʱ��Ϊ������ʱ��
                    flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                }
                else
                {
                    print("�����������0ʱ��δ�ҵ���������������");
                    dir = GetRandomDirection();
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
                    Debug.Log("������з���:"+dir);
                    target = null;
                    //�޸ķ���ʱ��Ϊ������ʱ��
                    flyTime = ConvertFlyDistanceToTime(afterbounceDistance, moveSpeed);
                }
            }
            else
            {
                print("�����������㣬�����ӵ���");
                Destroy(gameObject);
            }
        }
    }

}
