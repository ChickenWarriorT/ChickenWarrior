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

        public void Cast()
        {
            if (!IsOnCooldown())
            {
                for (int i = 0; i < skillData.castTimes; i++)
                {
                    ApplyEffects();
                }
                StartCooldown();
            }
        }
        public void AddEffect(SkillEffect skillEffect)
        {
            skillEffects.Add(skillEffect);
        }

        public void ApplyEffects()
        {
            // 遍历所有技能效果，并使用
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
