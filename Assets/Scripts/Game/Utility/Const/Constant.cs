using System.Collections.Generic;
using NPS.Tutorial;
using UnityEngine;

public static class Constant
{
    public static double Max = 9e307;

    public static Dictionary<HandType, string> HandType2Anim = new Dictionary<HandType, string>()
        { { HandType.Click, "Click" }, { HandType.Move, "idle" } };
}