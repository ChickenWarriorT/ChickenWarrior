using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Game Data/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int maxHealth;
    public float movementSpeed;
    // 其他怪物属性...


    public List<ChickenWarrior.Skill.SkillData> skills;
}
