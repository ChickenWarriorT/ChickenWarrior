using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private int health;
    [SerializeField]
    private int attakDamage;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int attackRange;
    protected Rigidbody2D rb;
    [SerializeField]
    private float defaultAttackCD;
    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private float attackCD;

    private Color _defaultColor;
    public int Health { get => health; set => health = value; }
    public int AttakeDamage { get => attakDamage; set => attakDamage = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackCD {
        get => SpeedToTimePerAttack(defaultAttackCD,attackSpeed);
    }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float DefaultAttackCD { get => defaultAttackCD; set => defaultAttackCD = value; }

    [SerializeField]
    private UnityEvent OnBeAttacked;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor=spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        
    }

    private float SpeedToTimePerAttack(float atkCD,float atkSpeed)
    {
        float defaultSpeed = 1.0f / atkCD;
        float currentSpeed = defaultSpeed * (1.0f + atkSpeed);
        float currentAtkCD = 1.0f/ currentSpeed;
        Debug.Log(currentAtkCD);
        return currentAtkCD;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (spriteRenderer)
            spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(_defaultColor);
        OnBeAttacked.Invoke();
    }

    
}
