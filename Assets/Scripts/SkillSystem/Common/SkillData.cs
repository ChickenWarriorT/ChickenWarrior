using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    // ��������
    [System.Serializable]
    public class SkillData
    {
        // ����ID
        public int skillID;
        // ��������
        public string name;
        // ��������
        public string description;
        // ������ȴ
        public float coolTime;
        // ��ȴʣ��
        public float coolRemain;
        // ��������
        public int cost;
        // ��������
        public float atkDistance;
        // ����Ŀ��tags
        public string[] attackTargetTags = { "Enemy" };
        // ����Ŀ���������
        [HideInInspector]
        public Transform[] attackTargets;
        // ����Ӱ������
        public string[] impactType = { "Cost", "Damage" };
        // �˺�����
        public float atkRatio;
        // ����ʱ��
        public float durationTime;
        // �˺����
        public float atkInterval;
        // ��������
        [HideInInspector]
        public GameObject owner;
        // ����Ԥ��������
        public string prefabName;
        // ����Ԥ����
        //[HideInInspector]
        public GameObject skillPrefab;
        // ��������
        public string animationName;
        // �ܻ���Ч����
        public string hitFxName;
        // �ܻ���ЧԤ����
        [HideInInspector]
        public GameObject hitFxPrfab;
        // �������� ���壬Ⱥ��
        public SkillAttackType attackType;
        // ѡ������ ���Σ�Բ�Σ�������
        public SelectorType selectorType;

        // �������ܲ���...
    }
}
