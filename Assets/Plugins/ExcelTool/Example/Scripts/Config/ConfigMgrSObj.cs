using System.Collections.Generic;
namespace ExcelData
{
	/// <summary>
	/// 数据管理类 : 脚本自动生成，勿手动修改
	/// <summary>
	[System.Serializable]
	public  class ConfigMgrSObj : UnityEngine.ScriptableObject
	{
		#region 变量

	/// <summary>
	/// Skill_BasicSkill
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("Skill_BasicSkill")]
	public Skill_BasicSkill[] skill_BasicSkills;
	/// <summary>
	/// Skill_SkillEffect
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("Skill_SkillEffect")]
	public Skill_SkillEffect[] skill_SkillEffects;


		#endregion

	}
}
