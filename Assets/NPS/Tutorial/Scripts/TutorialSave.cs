using System.Collections.Generic;

[System.Serializable]
public class TutorialSave : ADataSave
{
    public int CurTut = 0;
    public int CurStep = 0;

    public List<int> Complete = new List<int>();

    public TutorialSave(string key) : base(key)
    {

    }

    public override void Fix()
    {
        base.Fix();

        CurTut = 0;
        CurStep = 0;
    }
}
