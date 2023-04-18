using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Timers;

public class Player : Character
{
    private Vector2 _inputDerection;
    [SerializeField]
    private UnityEvent OnHealthChanged;

    private int findEnemiesAtOneTime = 10;

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private float weaponOffsetRange;

    private void Awake()
    {
        TimersManager.SetLoopableTimer(this, AttackCD, AutoAttack);
    }
    private void FixedUpdate()
    {
        Move();
    }

 

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputDerection = context.ReadValue<Vector2>();
    }
    public void Move()
    {
        //Vector2 position = transform.position;
        //Vector2 moveDir = _inputDerection.normalized;
        //Vector2 targetPosition = position + moveDir;

        //if (position != targetPosition)
        //{
        //    rb.DOMove(targetPosition, MoveSpeed).SetSpeedBased();
        //}

        Vector2 position = transform.position;
        Vector2 moveDir = _inputDerection.normalized;

        Vector2 targetPosition = position + moveDir;
        if (position != targetPosition)
            rb.DOMove(targetPosition, MoveSpeed).SetSpeedBased();
    }



    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged.Invoke();
    }

    public void RangedAttack()
    {
        if (bullet != null)
        {
            GameObject enemy = FindEnemy();
            if (enemy != null)
            {
                
                Vector2 offset = (enemy.transform.position - transform.position).normalized * weaponOffsetRange;
                Bullet bult=BulletManager._instance.CreateBullet(bullet, offset);
                bult.Init(gameObject, enemy, AttakeDamage);
            }
        }
    }

    public void AutoAttack()
    {
        RangedAttack();
    }

    public GameObject FindEnemy()
    {
        Collider2D[] results = new Collider2D[findEnemiesAtOneTime];

        Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange, results);

        foreach (var result in results)
        {
            if (result.CompareTag("Enemy"))
            {
                return result.gameObject;
            }
        }
        return null;
    }

}
