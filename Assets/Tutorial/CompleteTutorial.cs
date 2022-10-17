using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompleteTutorial : MonoBehaviour
{
    [SerializeField] private List<InitTutorialData> init;
    [SerializeField] private List<CompleteTutorialData> complete;

    private void Awake()
    {
        var save = DataManager.Save.Tutorial;
        foreach (var item in init)
        {
            if (save.Complete.Contains(item.Tut)) item.Complete?.Invoke();
            else item.UnComplete?.Invoke();
        }

        if (complete.Count > 0)
        {
            foreach (var item in complete)
            {
                TutorialManager.S.RegisterComplete(item.Tut, () =>
                {
                    item.Event?.Invoke();
                });
            }
        }
    }

    private void Start()
    {

    }
}

[System.Serializable]
public class CompleteTutorialData
{
    public int Tut;
    public UnityEvent Event;
}

[System.Serializable]
public class InitTutorialData
{
    public int Tut;
    public UnityEvent Complete;
    public UnityEvent UnComplete;
}