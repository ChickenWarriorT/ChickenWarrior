using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : BasePool<Bullet>
{
    private void Awake()
    {
        Initialize();
    }

    protected override Bullet OnCreatePoolItem()
    {
        var bullet = base.OnCreatePoolItem();
        bullet.SetDeactiveBullet(delegate { Release(bullet); });
        return bullet;
    }

    protected override void OnGetPoolItem(Bullet obj)
    {
        base.OnGetPoolItem(obj);
    }

}
