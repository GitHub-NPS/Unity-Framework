using UnityEngine;

public class ObjDummy : Dummy
{
    [SerializeField] private SpriteRenderer visual;

    public override void Set(string content)
    {
        visual.sprite = ResourceManager.S.LoadSprite("Currency", content);
    }
}
