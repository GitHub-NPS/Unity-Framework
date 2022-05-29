using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    public float Exit => exit;
    [SerializeField] private bool enable = false;
    [SerializeField] private bool pool = false;
    [SerializeField] private float exit = 10f;
    
    private CoroutineHandle handle;

    private void OnEnable()
    {
        if (enable) AutoDestroy();
    }

    public void Set(float exit)
    {
        this.exit = exit;
    }

    public void AutoDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_AutoDestroy().CancelWith(this.gameObject));
    }

    private void OnDisable()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    private IEnumerator<float> _AutoDestroy()
    {
        yield return Timing.WaitForSeconds(exit);
        if (pool) PoolManager.S.Despawn(this.gameObject);
        else Destroy(this.gameObject);
    }
}
