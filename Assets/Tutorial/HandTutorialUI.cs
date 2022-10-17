using System;
using MEC;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorialUI : HandTutorial
{
    [SerializeField] private SkeletonGraphic ga;

#if UNITY_EDITOR
    private void OnValidate()
    {
        ga = hand.GetComponent<SkeletonGraphic>();
    }
#endif

    public override void Set(HandType type)
    {
        ga.Initialize(false);

        ga.AnimationState.SetAnimation(0, Constant.HandType2Anim[type], true);
    }
}