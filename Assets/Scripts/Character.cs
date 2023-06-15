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
    [SerializeField]
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
    public float DefaultAttackCD { get => defaultAttackCD; set => defaultAttackCD = value; }


    private void Start()
    {
        Debug.Log("�ӽڵ�������-----------------------"+spriteRenderers.Length);
        rb = this.GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void ChangeSpriteColor(Color color)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOColor(color, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(Color.white);
        }
    }
    private void FixedUpdate()
    {

    }

    private void Update()
    {

    }
    public bool IsDie()
    {
        if (currentHealth <= 0)
            return true;
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
