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
    private Vector3 dir;

    private bool isMove = false;


    private float moveSpeed = 100;

    private Vector2 _moveDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
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

    private float currentFlyDistance;

    private GameObject caster;

    [SerializeField]
    private bool isTracing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 10);
    }


    private void Update()
    {
        transform.Translate(dir* Time.deltaTime*speed);
    }


    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        //currentTarget = target.transform;
        var tempDir = Vector3.Normalize(target.transform.position - this.transform.position);
        dir = new Vector3(tempDir.x,tempDir.y,0);
        startPosition = this.transform.position;

        ////修改子弹的朝向
        //transform.rotation = Quaternion.LookRotation(dir);

        isMove = true;
        //ChangeCurrentDirection(currentTarget);
    }


    private Enemy FindRandomEnemyNotIncludeTarget(Enemy target)
    {
        //TODO 确认怪物生成后是否加入了List 怪物死亡是否移除
        List<Enemy> enemyList = EnemyManager._instance.enemies;
        enemyList.Remove(target);
        if (enemyList.Count <= 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, enemyList.Count);
        Debug.Log(randomIndex);
        return enemyList[randomIndex];
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Character character = other.GetComponent<Character>();
            character.TakeDamage(damage);
            if (bounceCount > 0)
            {
                GetComponent<Collider2D>().enabled = false;
                bounceCount--;
                var tempEnemy = FindRandomEnemyNotIncludeTarget((Enemy)character);

                if (tempEnemy != null)
                {
                    print(tempEnemy.name);
                    var tempDir = (tempEnemy.transform.position - this.transform.position).normalized;
                    dir = new Vector3(tempDir.x, tempDir.y, 0);
                }
                else
                {
                    print("未找到怪物，销毁");
                    Destroy(gameObject);
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
