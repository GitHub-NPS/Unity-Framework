using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffExplodeLoop : EffExplode
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private AutoDestroyObject autoDestroy;
    private float step;

    public void SetAndRay(float step, float exit = -1)
    {
        if (exit != -1)
        {
            autoDestroy.Set(exit);
        }
        this.step = step;
        Timing.RunCoroutine(_Ray(this.step));
    }

    IEnumerator<float> _Ray(float step)
    {
        float time = autoDestroy.Exit;
        while (time > 0)
        {
            Ray();
            time -= step;
            yield return Timing.WaitForSeconds(step);
        }
    }
}
