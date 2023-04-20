using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Vector2 currentTarget;
    private Vector2 _moveDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

    [SerializeField]
    private int bounceCount;

    private GameObject caster;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        Vector2 position = (Vector2)transform.position + _moveDirection;
        if (position != currentTarget)
        {
            rb.DOMove(position, speed).SetSpeedBased();
        }

    }

    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        currentTarget = target.transform.position;
        _moveDirection = MoveDirectionPointToPoint(target.transform);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);
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

        int randomIndex = Random.Range(0, enemyList.Count);

        return enemyList[randomIndex];
    }

    private void ChangeCurrentDirection(Transform target)
    {
        _moveDirection = MoveDirectionPointToPoint(target);
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
                bounceCount -= 1;
                ChangeCurrentDirection(FindRandomEnemyNotIncludeTarget((Enemy)character).transform);
            }
            else
                Destroy();
        }

    }

}
