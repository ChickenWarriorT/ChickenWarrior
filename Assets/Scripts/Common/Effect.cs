using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private System.Action<Effect> deactiveEffect;
    private float durationTime;
    private float timer;

    private void Update()
    {
        DestoryOnTime();
    }
    public void Init(Vector2 pos, float time)
    {
        transform.position = pos;
        durationTime = time;
        timer = durationTime;
    }


    //到达特效持续时间销毁
    private void DestoryOnTime()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0.0f)
        {
            EffectDie();
        }

    }
    private void EffectDie()
    {
        deactiveEffect.Invoke(this);
    }
    public void SetDeactiveBullet(System.Action<Effect> deactiveAction)
    {
        deactiveEffect = deactiveAction;
    }
}
