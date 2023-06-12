using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Timers;
using System;

public enum MonsterType
{ 
    Monster01,
    Monster02
}
public class Monster : Character
{
    private bool _canCollide=true;
    private float _collideCD = 1.0f;
    
    private MonsterAI monsterAI=null;

    [SerializeField]
    private MonsterType monsterType;
    [SerializeField]
    private MovementType movementType;
    private void Awake()
    {
        //monsterAI = new MonsterAI(movementType, transform);
    }

    private void FixedUpdate()
    {
        //Move();
    }

    public void Init(Vector2 pos)
    {
        CurrentHealth = MaxHealth;
        transform.position = pos;
    }

    public void Move()
    {
        monsterAI.Move();
    }
    private void CanCollide()
    {
        _canCollide = true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Attack(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Attack(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CanCollide();
        }
    }

    public override void DestorySelf()
    {
        MonsterManager._instance.monsters.Remove(this);
        gameObject.SetActive(false);
    }

    private void Attack(Collider2D other)
    {
        if (!_canCollide) return;

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(AttakeDamage);
            TimersManager.SetTimer(this, _collideCD, CanCollide);
            _canCollide = false;
        }
    }
}
