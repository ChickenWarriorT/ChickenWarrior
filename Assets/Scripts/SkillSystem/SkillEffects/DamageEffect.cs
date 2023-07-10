using UnityEngine;

namespace ChickenWarrior.Skill
{
    public class DamageEffect : SkillEffect
    {
        private EffectData effectData;
        private SkillData skillData;

        public DamageEffect(EffectData effectData)
        {
            this.effectData = effectData;
        }

        public override void Apply(GameObject target)
        {
            throw new System.NotImplementedException();
        }
    }
}