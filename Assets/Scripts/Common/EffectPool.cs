using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : BasePool<Effect>
{
    private void Awake()
    {
        Initialize();
    }

    protected override Effect OnCreatePoolItem()
    {
        var effect = base.OnCreatePoolItem();
        effect.SetDeactiveBullet(delegate { Release(effect); });
        return effect;
    }

    protected override void OnGetPoolItem(Effect obj)
    {
        base.OnGetPoolItem(obj);
    }

}
