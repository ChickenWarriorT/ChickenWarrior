using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    public abstract class SkillEffect
    {
        public EffectData data;


        // ÿ������Ч��������ʵ�ֵķ���
        public abstract void Apply(GameObject target);
    }
}