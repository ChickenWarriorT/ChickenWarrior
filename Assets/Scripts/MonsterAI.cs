using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterAI
{
    private MonsterMovement movementAI;

    private ISkillAI skillAI;

    private MovementType movementType;

    //构造
    public MonsterAI(MovementType mType, Transform transform)
    {
        Init(mType, transform);
    }

    //初始化
    public void Init(MovementType mType, Transform transform)
    {
        //初始化移动AI
        movementType = mType;
        movementAI = new MonsterMovement(mType, transform);
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
