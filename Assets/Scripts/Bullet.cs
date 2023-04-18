using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Vector2 targetPosition;
    private Vector2 _moveDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

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
        if (position != targetPosition)
        {
            rb.DOMove(position, speed).SetSpeedBased();
        }
        
    }

    public void Init(GameObject caster, GameObject target, int damage)
    {
        this.caster = caster;
        this.damage = damage;
        targetPosition = target.transform.position;
        _moveDirection = MoveDirection(target.transform);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);
    }

    private Vector2 MoveDirection(Transform target)
    {

        if (target != null)
        {
           return  (target.position - transform.position).normalized;
        }
        return Vector3.zero;


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
                character.TakeDamage(damage);
                Destroy();
            }
        
    }

}
