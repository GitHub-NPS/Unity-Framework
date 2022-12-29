using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInforEventNoel : UIView
{
    protected override void Init()
    {
        base.Init();

        TutorialManager.S.RegisterInit(11, Hide);
        TutorialManager.S.RegisterInit(12, Hide);
        TutorialManager.S.RegisterInit(131, Hide);
    }
}
