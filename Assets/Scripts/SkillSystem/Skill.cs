using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    public class Skill
    {
        public SkillData skillData;
        // 技能效果字典，用于存储和查找技能效果
        private List<SkillEffect> skillEffects;

        public Skill(SkillData data)
        {
            this.skillData = data;

            List<EffectData> effectDataList = DataManager.LoadEffectData(skillData.skillID);
            foreach (EffectData effectData in effectDataList)
            {
                SkillEffect effect = SkillEffectFactory.CreateSkillEffect(effectData, skillData);
                AddEffect(effect);
            }

        }

        public void Activate()
        {
            if (!IsOnCooldown())
            {
                ApplyEffects();
                StartCooldown();
            }
        }
        public void AddEffect(SkillEffect skillEffect)
        {
            skillEffects.Add(skillEffect);
        }

        public void ApplyEffects()
        {
            // 根据data中的技能效果ID，从字典中获取对应的技能效果，并将其应用到游戏中
            foreach (var effect in skillEffects)
            {
                if (effect != null)
                    effect.Apply(skillData.target);
                else
                    Debug.LogWarning("No such effect with ID " + effect.data.id + "!");

            }
        }
        public void StartCooldown()
        {
            // 启动冷却计时
            // ...
        }

        public bool IsOnCooldown()
        {
            // 检查技能是否在冷却
            // ...
            return false;
        }
    }

    // 其他的方法...

}
