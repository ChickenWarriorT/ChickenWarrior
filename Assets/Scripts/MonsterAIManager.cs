using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIManager : MonoBehaviour
{
    [SerializeField]
    private MonsterMovement movementAI;

    private ISkillAI skillAI;

    private void Start()
    {
       
        
    }

    private void FixedUpdate()
    {
        // ���¹������Ϊ
        //Move();
        //UseSkill();
    }

    public void Move()
    {
        if (movementAI != null)
        {
            movementAI.Move();
        }
    }

    private void UseSkill()
    {
        if (skillAI != null)
        {
            skillAI.UseSkill();
        }
    }
}
