using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    private Animator animator;

    private bool isMove;

    public bool IsMove { get => isMove; set => isMove = value; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        animator.SetBool("IsMove", isMove);
    }

}
