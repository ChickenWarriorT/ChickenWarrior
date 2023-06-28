using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    // 技能数据
    [System.Serializable]
    public class SkillData
    {
        // 技能ID
        public int skillID;
        // 技能名称
        public string name;
        // 技能描述
        public string description;
        // 技能冷却
        public float coolTime;
        // 冷却剩余
        public float coolRemain;
        // 技能消耗
        public int cost;
        // 攻击距离
        public float atkDistance;
        // 攻击目标tags
        public string[] attackTargetTags = { "Enemy" };
        // 攻击目标对象数组
        [HideInInspector]
        public Transform[] attackTargets;
        // 技能影响类型
        public string[] impactType = { "Cost", "Damage" };
        // 伤害比率
        public float atkRatio;
        // 持续时间
        public float durationTime;
        // 伤害间隔
        public float atkInterval;
        // 技能所属
        [HideInInspector]
        public GameObject owner;
        // 技能预制体名称
        public string prefabName;
        // 技能预制体
        //[HideInInspector]
        public GameObject skillPrefab;
        // 动画名称
        public string animationName;
        // 受击特效名称
        public string hitFxName;
        // 受击特效预制体
        [HideInInspector]
        public GameObject hitFxPrfab;
        // 攻击类型 单体，群体
        public SkillAttackType attackType;
        // 选择类型 扇形（圆形），矩形
        public SelectorType selectorType;

        // 其他技能参数...
    }
}
