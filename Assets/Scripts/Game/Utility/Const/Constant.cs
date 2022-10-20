using System.Collections.Generic;
using NPS.Tutorial;

public static class Constant
{
    public static Dictionary<HandType, string> HandType2Anim = new Dictionary<HandType, string>() { { HandType.Click, "Click" }, { HandType.Move, "idle" } };

    public static double InitCoin = 1000;
    public static double InitDiamond = 0;
}
