using UnityEngine;

namespace ChickenWarrior.Skill
{
    public class HealingEffect : SkillEffect
    {
        private EffectData effectData;
        private SkillData skillData;

        public HealingEffect(EffectData effectData)
        {
            this.effectData = effectData;
        }

        public override void Apply(GameObject target)
        {
            throw new System.NotImplementedException();
        }
    }
}