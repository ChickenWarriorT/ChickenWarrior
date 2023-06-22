using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullReaper : Monster
{

    [SerializeField]
    private float retreatDistance = 5f; // �󳷵ľ���
    [SerializeField]
    private float retreatSpeed = 5f; // �󳷵��ٶ�
    [SerializeField]
    public float chargeSpeed = 10f; // ��̵��ٶ�
    [SerializeField]
    public float chargeDistance = 10f; // ��̵ľ���



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
    // ����
    public override void Skill()
    {
        base.Skill();
        // �����������ִ�У����ͷ��µļ���;���������CD��
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

    // ִ�м��� 
    IEnumerator PerformSkill()
    {
        // ��ʼִ�м���
        isSkillPerforming = true;
        Vector3 directionToPlayer = Utilities.DirectionFromAToB(transform, PlayerManager._instance.Player.transform);
        float retreatDirection = (PlayerManager._instance.Player.transform.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(retreatDirection, 1, 1);
        //��
        Vector2 retreatStartPosition = transform.position;
        while (Vector3.Distance(transform.position, retreatStartPosition) < retreatDistance)
        {
            //�������������Э��
            if (IsDie()) yield break;
            // �ƶ�����
            transform.position -= directionToPlayer * retreatSpeed * Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // �ȴ�һ֡
        yield return null;

        // ���
        Vector3 chargeStartPosition = transform.position;
        while (Vector3.Distance(transform.position, chargeStartPosition) < chargeDistance)
        {
            //�������������Э��
            if (IsDie()) yield break;
            // �ƶ�����
            transform.position += directionToPlayer * chargeSpeed * Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }
        isSkillPerforming = false;

        //���ܽ�������ʼ������ȴ
        attackTimer = DefaultAttackCD;
        
    }
}
