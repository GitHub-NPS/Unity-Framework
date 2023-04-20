using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public class ConfigCamera : LeanDragCamera
{
    public static ConfigCamera S;

    [SerializeField] private float size = 9.6f;
    [SerializeField] private Vector2 clamp = new Vector2(-10f, 10f);

    private enum ScreenType
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private ScreenType screenType;
    private Vector2 screenSize = Vector2.zero;
    private float ratioScaleScreen = 0f;

    public Vector2 ScreenSize
    {
        get
        {
            if (screenSize.x > 0)
                return screenSize;
            screenSize.x = Screen.width;
            screenSize.y = Screen.height;
            return screenSize;
        }
        private set => screenSize = value;
    }

    public float RatioScaleScreen
    {
        get
        {
            if (ratioScaleScreen > 0)
                return ratioScaleScreen;
            ratioScaleScreen = GetRatio();
            return ratioScaleScreen;
        }
        private set => ratioScaleScreen = value;
    }

    protected override void Awake()
    {
        if (!S) S = this;

        Initialize();
        ScaleCamera();

        base.Awake();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        var pos = transform.position;
        pos.x = 0;
        pos.y = Mathf.Clamp(transform.position.y, clamp.x, clamp.y);
        transform.position = pos;
    }

    public void MoveY(float positionY, Action complete = null)
    {
        float duration = Math.Abs(this.transform.position.y - positionY) / 10f;
        this.transform.DOMoveY(positionY, duration).SetEase(Ease.OutSine).OnComplete(() => { complete?.Invoke(); });
    }

    private void ScaleCamera()
    {
        size *= RatioScaleScreen;
        Camera.main.orthographicSize = size;

        clamp /= RatioScaleScreen;
    }

    private void Initialize()
    {
        RatioScaleScreen = GetRatio();
        //Debug.LogWarning("Screen Size -- " + ScreenSize + " -- " + RatioScaleScreen);
    }

    private float GetRatio()
    {
        if (screenType == ScreenType.Vertical)
        {
            var deviceRatio = ScreenSize.x / ScreenSize.y;
            var deviceWidth = 1080 / deviceRatio;
            return deviceWidth / 1920;
        }
        else
        {
            var deviceRatio = ScreenSize.x / ScreenSize.y;
            var deviceHeight = 1920 / deviceRatio;
            return deviceHeight / 1080;
        }
    }
}