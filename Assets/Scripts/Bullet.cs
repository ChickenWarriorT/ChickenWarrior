using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Transform currentTarget;
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
    private float afterbounceDistance = 99;

    [SerializeField]
    private float flyDistance;

    private float currentFlyDistance;

    private GameObject caster;

    [SerializeField]
    private bool isTracing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DestroyExcceedDistance();
        Move();
    }

    private void Move()
    {

        Vector2 position = (Vector2)transform.position + _moveDirection;
        if (position != (Vector2)currentTarget.transform.position)
        {
            rb.DOMove(position, speed).SetSpeedBased();
        }
        if (isTracing)
        {
            if (currentTarget != null)
                TracingTarget(currentTarget);
        }

    }

    private void DestroyExcceedDistance()
    {
        currentFlyDistance = ((Vector2)transform.position - startPosition).magnitude;
        if (currentFlyDistance > flyDistance)
            Destroy();
    }

    private void TracingTarget(Transform target)
    {
        ChangeCurrentDirection(target);
    }


    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        currentTarget = target.transform;
        startPosition = this.transform.position;
        ChangeCurrentDirection(currentTarget);
    }

    private Vector2 MoveDirectionPointToPoint(Transform target)
    {

        if (target != null)
        {
            return (target.position - transform.position).normalized;
        }
        return Vector3.zero;
    }

    private Enemy FindRandomEnemyNotIncludeTarget(Enemy target)
    {
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

    private void FindCurrentEnemyDirection(Enemy character)
    {
        
    }


    public Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    private void ChangeCurrentDirection(Transform target)
    {
        if (target != null)
        {
            _moveDirection = MoveDirectionPointToPoint(target);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        }

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();

        if (other.CompareTag("Enemy"))
        {
            if (bounceCount > 0)
            {
                character.TakeDamage(damage);
                GetComponent<Collider2D>().enabled = false;
                bounceCount -= 1;
                flyDistance = afterbounceDistance;
                currentFlyDistance = 0.0f;
                currentTarget = FindRandomEnemyNotIncludeTarget((Enemy)character).transform;

                if (currentTarget == null)
                {
                    _moveDirection = GetRandomDirection();
                }
                else
                    ChangeCurrentDirection(currentTarget);
            }
            else
                Destroy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
