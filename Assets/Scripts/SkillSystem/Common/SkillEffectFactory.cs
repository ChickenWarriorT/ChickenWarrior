using System;

namespace ChickenWarrior.Skill
{
    public class SkillEffectFactory
    {
        public static SkillEffect CreateSkillEffect(EffectData effectData, SkillData skillData)
        {
            switch (effectData.effectType)
            {
                case "Damage":
                    return new DamageEffect(effectData);
                case "Healing":
                    return new HealingEffect(effectData);
                // 可以添加更多的 case 语句来处理其他类型的效果
                default:
                    throw new ArgumentException($"Unknown effect type: {effectData.effectType}");
            }
        }
    }
}