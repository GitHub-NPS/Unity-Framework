using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private List<StartTutorialData> data;

    private void Awake()
    {
        if (data.Count > 0)
        {
            foreach (var item in data)
            {
                TutorialManager.S.RegisterInit(item.Tut, () =>
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
public class StartTutorialData
{
    public int Tut;
    public UnityEvent Event;
}