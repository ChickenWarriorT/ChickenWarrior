using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Timers;
using System;

public enum MonsterType
{
    SkullReaper,
    Monster02
}
public class Monster : Character
{
    private bool _canCollide=true;
    private float _collideCD = 1.0f;

    //°²È«¾àÀë
    [SerializeField]
    private float safeDistance;


    //private MonsterAI monsterAI=null;
    [SerializeField]
    private MonsterType monsterType;
    [SerializeField]
    private MovementType movementType;

    public float SafeDistance { get => safeDistance; set => safeDistance = value; }
    public float AwayDistance { get => Mathf.Clamp(safeDistance-0.1f,0.0f,safeDistance); }



    protected override void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        base.Init();
        CurrentHealth = MaxHealth;
    }


    public void Move()
    {
        //monsterAI.Move();
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

    public override void Die()
    {
        base.Die();
    }
    public override void DestorySelf()
    {
        MonsterManager._instance.monsters.Remove(this);
        gameObject.SetActive(false);
    }

    public override void Skill()
    {
        base.Skill();
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
