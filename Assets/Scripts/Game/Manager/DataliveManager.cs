using System.Collections;
using System.Collections.Generic;
using NPS;
using UnityEngine;

public class DataliveManager : MonoSingleton<DataliveManager>
{
    //public AbilityLive Ability;

    private void Start()
    {

    }

    public void Init(Transform parent = null)
    {
        DataManager.Live = this;
        if (parent) transform.SetParent(parent);

        //Ability = Resources.Load<AbilityLive>("Live/AbilityLive");

        ClearAll();
    }

    public void ClearAll()
    {
        //Ability.Value.Clear();
    }
}
