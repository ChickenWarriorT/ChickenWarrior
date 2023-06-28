using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChickenWarrior.Skill
{
    /// <summary>
    /// ���ܹ�����
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        //�����б�
        public SkillData[] skills;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
                InitSkill(skills[i]);


        }

        //��ʼ������
        private void InitSkill(SkillData data)
        {
            data.skillPrefab = Resources.Load<GameObject>("Skills/" + data.prefabName);
            data.owner = gameObject;
        }

        //���ɼ���
        public void GenerateSkill(SkillData data)
        {
            //��������Ԥ����
            GameObject skill = Instantiate(data.skillPrefab, transform.position, transform.rotation);

            //���ټ���
            Destroy(skill, data.durationTime);

            //��ʼ������ȴ
            StartCoroutine(CoolTimeDown(data));
        }

        //׼������
        public SkillData PrepareSKill(int id)
        {
            //����id ���Ҽ�������
            SkillData data = Array.Find(skills, s => s.skillID == id);
            //�ж����� ���ؼ�������
            if (data != null && data.coolRemain <= 0)
                return data;
            else
                return null;
        }

        //������ȴ
        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1f);
                data.coolRemain--;
            }
        }
    }
}
