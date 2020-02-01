using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEffectManager : MonoBehaviour {

    public static TimeEffectManager Inst;

    private void Awake()
    {
        Inst = this;
    }

    private void Update()
    {
        //if (Time.unscaledTime - slowMoActivationTime <= DataHolder.Data.SlowMoTime)
        //{
        //    float ratio = (Time.unscaledTime - slowMoActivationTime) / DataHolder.Data.SlowMoTime;
        //    float timeScale = DataHolder.Data.TimeScale * SlowMoCurve.Evaluate(ratio);
        //    if (timeScale >= 0)
        //        Time.timeScale = timeScale;
        //}


    }

    float slowMoActivationTime = 0;
    // public float SlowMoTime = 0.2f;
    public AnimationCurve SlowMoCurve;

    public void ActivateSlowMo()
    {
        //slowMoActivationTime = Time.unscaledTime;
    }
}
