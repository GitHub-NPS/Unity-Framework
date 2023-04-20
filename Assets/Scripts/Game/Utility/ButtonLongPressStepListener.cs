using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLongPressStepListener : ButtonLongPressListener
{
    [SerializeField] private float timePress = 2f;

    private float t = 0;
    private Action actionStep;
    private Func<bool> conditionStep;

    public void SetTimePress(float timePress)
    {
        this.timePress = timePress;
    }

    public void Step(Action action, Func<bool> condition)
    {
        this.actionStep = action;
        this.conditionStep = condition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        t = 0;

        base.OnPointerDown(eventData);
    }

    protected override void iAction()
    {
        base.iAction();

        t += second;

        if (t >= timePress)
        {
            t = 0;
            if (actionStep != null && conditionStep != null)
            {
                if (conditionStep.Invoke())
                {
                    actionStep?.Invoke();
                }
            }
        }
    }
}
