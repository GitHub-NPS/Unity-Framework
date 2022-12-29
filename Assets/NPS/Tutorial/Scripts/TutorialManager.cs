using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;
using NPS.Tutorial;

public class TutorialManager : Manager
{
    private UserSave userSave;

    protected override void Awake()
    {
        userSave = DataManager.Save.User;
        base.Awake();
    }

    protected override bool iConditionInit(int tut)
    {
        switch (tut)
        {
            case 1:
                return true;
        }

        return false;
    }

    public override bool Handler(int tut, int step)
    {
        switch (tut)
        {
            case 1:
                {
                    switch (step)
                    {
                        case 1:
                            UI.HideHand();
                            Complete(save.CurTut);
                            return true;
                    }
                }
                break;
        }

        return false;
    }
}