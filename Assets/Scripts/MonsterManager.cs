using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager _instance;

    [SerializeField]
    public List<Monster> monsters;
    private void Awake()
    {
        _instance = this;
    }

    //·µ»ØËæ»ú¹ÖÎï
    public Monster GetRandomMonster()
    {
        if (monsters.Count > 0)
        {
            int r = Random.Range(0, monsters.Count);
        }
        return null;
    }
}
