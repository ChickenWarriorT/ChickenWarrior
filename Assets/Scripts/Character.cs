using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
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

    private Color _defaultColor;

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
    

    //[SerializeField]
    //protected UnityEvent OnBeAttacked;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = spriteRenderer.color;
    }
    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (IsDie())
        {
            DestorySelf();
        }
    }
    public bool IsDie()
    {
        if (currentHealth <= 0)
            return true;
        return false;
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
        if (spriteRenderer)
            spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(_defaultColor);
        //OnBeAttacked.Invoke();
    }



}
