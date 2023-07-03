using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Timers;
using ChickenWarrior.Skill;

public class Player : Character
{
    private Vector2 _inputDerection;
    [SerializeField]
    private UnityEvent OnHealthChanged;

    private int findEnemiesAtOneTime = 10;

    [SerializeField]
    private BulletSpawner bulletSpawner;
    [SerializeField]
    private string bulletName;
    [SerializeField]
    private float weaponOffsetRange;

    private Vector2 targetPosition;


    private void Awake()
    {
        TimersManager.SetLoopableTimer(this, AttackCD, AutoAttack);
    }

    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
        Init();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        //��J�ͷż���
        if (Input.GetKeyDown(KeyCode.J))
        {
            SkillManager skillManager = GetComponent<SkillManager>();
            SkillData data = skillManager.PrepareSKill(1001);
            if (data != null)
            {
                skillManager.GenerateSkill(data);
            }
        }
    }

    public override void Init()
    {
        base.Init();
        CurrentHealth = MaxHealth;
        OnHealthChanged.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputDerection = context.ReadValue<Vector2>();
    }
    public void Move()
    {

        Vector2 position = transform.position;
        Vector2 moveDir = _inputDerection.normalized;
        Vector2 targetPosition = position + moveDir;

        targetPosition = MapManager._instance.PosRestrainInBoundary(targetPosition);

        if (position != targetPosition)
            transform.position = Vector2.MoveTowards(position, targetPosition, MoveSpeed * Time.fixedDeltaTime);

    }




    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged.Invoke();

    }

    public override void DestorySelf()
    {

    }

    //Զ�̹���
    public void RangedAttack()
    {
        if (bulletSpawner != null)
        {
            GameObject enemy = FindEnemy();
            if (enemy != null)
            {
                //�ӵ����������ɫ�ľ���
                Vector2 offset = (enemy.transform.position - transform.position).normalized * weaponOffsetRange;
                bulletSpawner.SpawnBullet(bulletName, (Vector2)transform.position + offset, enemy, AttakeDamage);
            }
        }
    }



    public void AutoAttack()
    {
        RangedAttack();
    }

    //Ѱ�ҹ�����Χ������ĵз���λ
    public GameObject FindEnemy()
    {
        Collider2D[] results = new Collider2D[findEnemiesAtOneTime];

        Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange, results);

        if (results != null)
        {
            foreach (var result in results)
            {
                if (result != null && result.CompareTag("Enemy"))
                {
                    return result.gameObject;
                }
            }
        }
        return null;
    }

}
