using BayatGames.SaveGameFree;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using NPS.Pattern.Observer;

[System.Serializable]
public class TutorialSave : IDataSave
{
    public string Key => key;
    public string key;

    public TutorialSave(string key)
    {
        this.key = key;
    }

    [Button]
    public void Save()
    {
        SaveGame.Save(Key, this);
    }

    public int CurTut = 0;
    public int CurStep = 0;

    public List<int> Complete = new List<int>();

    public void Fix()
    {
        if (Complete == null) Complete = new List<int>();

        CurTut = 0;
        CurStep = 0;
    }

#if UNITY_EDITOR
    [Button]
    public void Test()
    {
        Observer.S?.PostEvent(EventID.StartTutorial, 999);
    }
#endif
}
