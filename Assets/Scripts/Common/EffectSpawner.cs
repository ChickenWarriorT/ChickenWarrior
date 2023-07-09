using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : BaseSpawner<Effect, EffectPool>
{
    public static EffectSpawner _instance;

    private void Awake()
    {
        _instance = this;
    }
    public Effect SpawnEffect(string prefabName, Vector2 pos,float durationTime)
    {
        Effect effect = objectPools[prefabName].Get();
        effect.Init(pos, durationTime);
        return effect;
    }
}
