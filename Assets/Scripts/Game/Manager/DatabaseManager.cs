using UnityEngine;
using com.unimob.pattern.singleton;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class DatabaseManager : MonoSingleton<DatabaseManager>
{
    public GeneralTable General = new GeneralTable();
    public GemTable Gem = new GemTable();

    private void Start()
    {
    }

    public void Init(Transform parent = null)
    {
        DataManager.Base = this;
        if (parent) transform.SetParent(parent);

        General.GetDatabase();
        Gem.GetDatabase();
    }
}