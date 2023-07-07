using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager _instance;

    [SerializeField]
    private List<Monster> monsters;

    public List<Monster> Monsters { get => monsters; set => monsters = value; }

    private void Awake()
    {
        _instance = this;
    }

    public void AddMonster(Monster monster)
    {
        monsters.Add(monster);
    }
    public void RemoveMonster(Monster monster)
    {
        monsters.Remove(monster);
    }

    public int GetMonstersCount()
    {
        return monsters.Count;
    }

    //·µ»ØËæ»ú¹ÖÎï
    public Monster GetRandomMonster()
    {
        if (monsters.Count > 0)
        {
            int r = Random.Range(0, monsters.Count);
            return monsters[r];
        }
        return null;
    }

    public List<Monster> GetActiveMonster()
    {
        List<Monster> activeMonsters = new List<Monster>();
        foreach (var monster in monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                activeMonsters.Add(monster);
            }
        }
        return activeMonsters;
    }
}
