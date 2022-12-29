using BayatGames.SaveGameFree;
using UnityEngine;

public abstract class ADataSave
{
    public string Key => key;
    public string key = null;

    public ADataSave(string key)
    {
        this.key = key;
    }

    public virtual void Fix()
    {
        if (string.IsNullOrEmpty(key))
            key = this.GetType().Name.Replace("Save", "");
    }

    public virtual void Save()
    {
        SaveGame.Save(key, this);
    }
}
