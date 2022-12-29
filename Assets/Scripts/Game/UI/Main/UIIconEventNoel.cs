using TMPro;
using UnityEngine;

public class UIIconEventNoel : UIIconEventItem
{
    public override void OnClick()
    {
        MainGameScene.S.Show<UIEventNoel>();
    }
}
