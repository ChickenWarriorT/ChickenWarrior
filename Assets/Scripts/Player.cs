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
        //按J释放技能
        if (Input.GetKeyDown(KeyCode.J))
        {
            SkillManager skillManager = GetComponent<SkillManager>();
            //SkillData data = skillManager.PrepareSKill(1001);
            //if (data != null)
            //{
            //    skillManager.GenerateSkill(data);
            //}
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AutoAttack();
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

    //远程攻击
    public void RangedAttack()
    {
        if (bulletSpawner != null)
        {
            GameObject enemy = FindEnemy();
            if (enemy != null)
            {
                //子弹出生点离角色的距离
                Vector2 offset = (enemy.transform.position - transform.position).normalized * weaponOffsetRange;
                bulletSpawner.SpawnBullet(bulletName, (Vector2)transform.position + offset, enemy, AttakeDamage);
            }
        }
    }



    public void AutoAttack()
    {
        RangedAttack();
    }

    //寻找攻击范围内最近的敌方单位
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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 30), "Fire_III_Flame_B"))
        {
            SkillManager skillManager = GetComponent<SkillManager>();
            //SkillData data = skillManager.PrepareSKill(1001);
            //if (data != null)
            //{
            //    skillManager.GenerateSkill(data);
            //}
        }
        if (GUI.Button(new Rect(10, 50, 150, 30), "Fire_III_Flame_C"))
        {
            SkillManager skillManager = GetComponent<SkillManager>();
            //SkillData data = skillManager.PrepareSKill(1002);
            //if (data != null)
            //{
            //    skillManager.GenerateSkill(data);
            //}
        }
    }
}
