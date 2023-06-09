using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChickenWarrior.Skill
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        //    //技能列表
        //    public SkillData[] skills;

        //    private void Start()
        //    {
        //        for (int i = 0; i < skills.Length; i++)
        //            InitSkill(skills[i]);
        //    }

        //    //初始化技能
        //    private void InitSkill(SkillData data)
        //    {
        //        data.skillPrefab = Resources.Load<GameObject>("Skills/" + data.prefabName);
        //        data.owner = gameObject;
        //    }

        //    //生成技能
        //    public void GenerateSkill(SkillData data)
        //    {
        //        //创建技能预制体

        //        Effect effect = EffectSpawner._instance.SpawnEffect(data.prefabName,transform.position,data.durationTime);
        //        SkillDeployer deployer = new SkillDeployer(data);
        //        //SkillDeployer deployer = skills

        //        //开始技能冷却
        //        StartCoroutine(CoolTimeDown(data));
        //    }

        //    //准备技能
        //    public SkillData PrepareSKill(int id)
        //    {
        //        //根据id 查找技能数据
        //        SkillData data = Array.Find(skills, s => s.skillID == id);
        //        //判断条件 返回技能数据
        //        if (data != null && data.coolRemain <= 0)
        //            return data;
        //        else
        //            return null;
        //    }

        //    //技能冷却
        //    private IEnumerator CoolTimeDown(SkillData data)
        //    {
        //        data.coolRemain = data.coolTime;
        //        while (data.coolRemain > 0)
        //        {
        //            yield return new WaitForSeconds(1f);
        //            data.coolRemain--;
        //        }
        //    }
        //}

        // 存储所有的技能
        [SerializeField]
        private Dictionary<int, Skill> skills = new Dictionary<int, Skill>();

        // 加载和初始化所有的技能
        public void LoadSkills()
        {
            List<SkillData> allSkillData = DataManager.LoadAllSkillData();

            foreach (SkillData skillData in allSkillData)
            {
                Skill skill = new Skill(skillData);

                skills[skillData.skillID] = skill;
            }
        }

        // 根据技能ID获取技能
        public Skill GetSkill(int skillID)
        {
            if (skills.ContainsKey(skillID))
            {
                return skills[skillID];
            }
            else
            {
                return null;
            }
        }
    }
}
