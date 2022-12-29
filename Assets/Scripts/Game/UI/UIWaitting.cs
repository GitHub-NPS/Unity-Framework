using com.unimob.mec;
using com.unimob.timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWaitting : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform progress;

    private float speed = -300f;
    private TickData tick = new TickData();
    private CoroutineHandle handleKill;

    public void Show(float exit = -1)
    {
        if (handleKill.IsValid) Timing.KillCoroutines(handleKill);

        //progress.rotation = Quaternion.Euler(Vector3.zero);        
        tick.Action = Tick;
        tick.RegisterTick();

        content.SetActive(true);

        if (exit > 0) handleKill = Timing.RunCoroutine(_Kill(exit));
    }

    private IEnumerator<float> _Kill(float time)
    {
        yield return Timing.WaitForSeconds(time);

        Hide();
    }

    private void Tick()
    {
        progress.Rotate(0f, 0f, speed * Timing.DeltaTime);
    }

    public void Hide()
    {
        if (handleKill.IsValid) Timing.KillCoroutines(handleKill);
        content.SetActive(false);
        tick.RemoveTick();
    }
}
