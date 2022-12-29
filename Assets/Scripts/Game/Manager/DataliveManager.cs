using System.Collections;
using System.Collections.Generic;
using com.unimob.pattern.singleton;
using UnityEngine;

public class DataliveManager : MonoSingleton<DataliveManager>
{
    public GameConfig GameConfig;

    private void Start()
    {

    }

    public void Init(Transform parent = null)
    {
        DataManager.Live = this;
        if (parent) transform.SetParent(parent);

        GameConfig = Resources.Load<GameConfig>("Live/GameConfig");

        ClearAll();
    }

    public void ClearAll()
    {

    }
}
