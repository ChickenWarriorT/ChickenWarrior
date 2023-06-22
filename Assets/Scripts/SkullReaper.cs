using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullReaper : Monster
{

    [SerializeField]
    private float retreatDistance = 5f; // 后撤的距离
    [SerializeField]
    private float retreatSpeed = 5f; // 后撤的速度
    [SerializeField]
    public float chargeSpeed = 10f; // 冲刺的速度
    [SerializeField]
    public float chargeDistance = 10f; // 冲刺的距离



    private float attackTimer = 0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            skillInCD = true;
        }
        else
            skillInCD = false;
    }
    // 技能
    public override void Skill()
    {
        base.Skill();
        // 如果技能正在执行，不释放新的技能;如果技能在CD内
        if (isSkillPerforming || skillInCD) return;

        StartCoroutine(PerformSkill());
    }

    public override void Init()
    {
        base.Init();
        isSkillPerforming = false;
        attackTimer = 0;
        skillInCD = false;
    }

    // 执行技能 
    IEnumerator PerformSkill()
    {
        // 开始执行技能
        isSkillPerforming = true;
        Vector3 directionToPlayer = Utilities.DirectionFromAToB(transform, PlayerManager._instance.Player.transform);
        float retreatDirection = (PlayerManager._instance.Player.transform.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(retreatDirection, 1, 1);
        //后撤
        Vector2 retreatStartPosition = transform.position;
        while (Vector3.Distance(transform.position, retreatStartPosition) < retreatDistance)
        {
            //如果死亡，跳出协成
            if (IsDie()) yield break;
            // 移动怪物
            transform.position -= directionToPlayer * retreatSpeed * Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 等待一帧
        yield return null;

        // 冲刺
        Vector3 chargeStartPosition = transform.position;
        while (Vector3.Distance(transform.position, chargeStartPosition) < chargeDistance)
        {
            //如果死亡，跳出协成
            if (IsDie()) yield break;
            // 移动怪物
            transform.position += directionToPlayer * chargeSpeed * Time.deltaTime;
            yield return null; // 等待下一帧
        }
        isSkillPerforming = false;

        //技能结束，开始计算冷却
        attackTimer = DefaultAttackCD;
        
    }
}
