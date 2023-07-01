using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : BasePool<Monster>
{
    private void Awake()
    {
        Initialize();
    }

    protected override Monster OnCreatePoolItem()
    {
        var monster = base.OnCreatePoolItem();
        Release(monster);
        return monster;
    }

    protected override void OnGetPoolItem(Monster obj)
    {
        base.OnGetPoolItem(obj);
    }
}
