using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    public class Skill
    {
        public SkillData skillData;
        // ����Ч���ֵ䣬���ڴ洢�Ͳ��Ҽ���Ч��
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
            // ����data�еļ���Ч��ID�����ֵ��л�ȡ��Ӧ�ļ���Ч����������Ӧ�õ���Ϸ��
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
            // ������ȴ��ʱ
            // ...
        }

        public bool IsOnCooldown()
        {
            // ��鼼���Ƿ�����ȴ
            // ...
            return false;
        }
    }

    // �����ķ���...

}
