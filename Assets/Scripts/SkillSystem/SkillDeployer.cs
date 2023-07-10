using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenWarrior.Skill
{
    public class SkillDeployer : MonoBehaviour
    {
        private SkillData skilldata;

        public SkillDeployer(SkillData data)
        {
            skilldata = data;
        }

        public SkillData Skilldata { get => skilldata; set => skilldata = value; }
    }
}
