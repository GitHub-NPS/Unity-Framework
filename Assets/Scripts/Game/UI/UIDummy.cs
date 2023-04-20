using UnityEngine;
using UnityEngine.UI;

public class UIDummy : Dummy
{
    [SerializeField] private Image visual;

    public override void Set(string content)
    {
        visual.sprite = ResourceManager.S.LoadSprite("Currency", content);
    }
}
