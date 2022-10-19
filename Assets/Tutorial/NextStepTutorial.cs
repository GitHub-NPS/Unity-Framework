using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NextStepTutorial : MonoBehaviour
{
    [SerializeField] private List<NextStepTutorialData> data;

    private void Awake()
    {
        if (data.Count > 0)
        {
            foreach (var item in data)
            {
                TutorialManager.S.RegisterNext(item.Tut, item.Step, () =>
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
public class NextStepTutorialData
{
    public int Tut;
    public int Step;
    public UnityEvent Event;
}