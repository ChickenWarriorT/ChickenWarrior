using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    public abstract class SkillEffect
    {
        public EffectData data;


        // 每个技能效果都必须实现的方法
        public abstract void Apply(GameObject target);
    }
}