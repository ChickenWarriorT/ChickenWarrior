using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    protected SpriteRenderer[] spriteRenderers;
    //���Ѫ��
    [SerializeField]
    private int maxHealth;
    //��ǰѪ��
    [SerializeField]
    private int currentHealth;
    //�˺�
    [SerializeField]
    private int attakDamage;
    //�ƶ��ٶ�
    [SerializeField]
    private float moveSpeed;
    //��������
    [SerializeField]
    private int attackRange;
    protected Rigidbody2D rb;
    //��ʼ����CD
    [SerializeField]
    private float defaultAttackCD;
    //�����ٶ�
    [SerializeField]
    private float attackSpeed;
    //����CD
    [SerializeField]
    private float attackCD;

    protected bool skillInCD = false;

    protected Animator animator;
    protected Animation _animation;

    protected bool isSkillPerforming = false;

    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int AttakeDamage { get => attakDamage; set => attakDamage = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackCD
    {
        get => SpeedToTimePerAttack(defaultAttackCD, attackSpeed);
    }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float DefaultAttackCD { get => defaultAttackCD; }
    public bool IsSkillPerforming { get => isSkillPerforming; }

    protected virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _animation = GetComponent<Animation>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        //Debug.Log("�ӽڵ�������-----------------------" + spriteRenderers.Length);
    }

    private void ChangeSpriteColor(Color color)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOColor(color, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(Color.white);
        }
    }

    //�ж�����
    public bool IsDie()
    {
        if (currentHealth <= 0)
            return true;
        return false;
    }
    //�ж��ɹ���Ŀ��
    public bool CanAttack(Transform target)
    {
        //������,�ͼ���CD���
        if (Vector2.Distance(target.position, transform.position) <= attackRange && !skillInCD)
            return true;
        return false;
    }
    public virtual void Skill()
    {

    }

    public virtual bool IsFinishSkill()
    {
        return false;
    }
    public virtual void Die()
    {
        DestorySelf();
    }
    public virtual void DestorySelf()
    {
        Destroy(this.gameObject);
    }

    public virtual void Init()
    {

    }

    private float SpeedToTimePerAttack(float atkCD, float atkSpeed)
    {
        float defaultSpeed = 1.0f / atkCD;
        float currentSpeed = defaultSpeed * (1.0f + atkSpeed);
        float currentAtkCD = 1.0f / currentSpeed;
        Debug.Log(currentAtkCD);
        return currentAtkCD;
    }


    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (spriteRenderers.Length > 0)
        {
            ChangeSpriteColor(Color.red);
            //spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(_defaultColor);
        }
        //OnBeAttacked.Invoke();
    }



}
