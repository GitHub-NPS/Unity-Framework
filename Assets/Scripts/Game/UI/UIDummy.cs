using UnityEngine;
using UnityEngine.UI;

public class UIDummy : Dummy
{
    [SerializeField] private Image icon;

    public override void Set(string content)
    {
        icon.sprite = ResourceManager.S.LoadSprite("Currency", content);
    }
}
