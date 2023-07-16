using System.Collections.Generic;
namespace ExcelData
{
	/// <summary>
	/// Skill_SkillEffect
	/// <summary>
	[System.Serializable]
	public  class Skill_SkillEffect
	{
		#region 变量

	/// <summary>
	/// 技能ID
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能ID")]
	public int ID;
	/// <summary>
	/// 技能名称
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能名称")]
	public string SkillName;
	/// <summary>
	/// 索敌类型
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("索敌类型")]
	public int SearchType;
	/// <summary>
	/// 索敌范围
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("索敌范围")]
	public int SearchRange;
	/// <summary>
	/// 技能释放范围
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能释放范围")]
	public float SkillReleaseRange;
	/// <summary>
	/// 技能打击范围
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能打击范围")]
	public int SkillRange;
	/// <summary>
	/// 目标类型
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("目标类型")]
	public int TargetType;
	/// <summary>
	/// 索敌逻辑
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("索敌逻辑")]
	public int SearcchLogic;
	/// <summary>
	/// 目标数量
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("目标数量")]
	public int TargetNum;
	/// <summary>
	/// 施法类型
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("施法类型")]
	public int CastType;
	/// <summary>
	/// 技能冷却
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能冷却")]
	public int SkillCD;
	/// <summary>
	/// 技能初始冷却
	/// <summary>
	[UnityEngine.SerializeField, UnityEngine.Header("技能初始冷却")]
	public int SkillDefaultCD;


		#endregion

	}
}
