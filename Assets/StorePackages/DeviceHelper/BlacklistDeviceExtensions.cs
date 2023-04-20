using System.Linq;

public static class BlacklistDeviceExtensions
{
    private static readonly string[] Blacklist =
    {
        "01cde2d39e580d24",
        "455008ba69d0fed3",
        "194b0d1987ff24ea",
        "659d195b61d8a64c",
        "659d195b61d8a64c",
        "bfb142ab26b1c17f",
        "43881eb5d9f38e90",
        "e05c7c67d62c9deb",
        "a53d08478e8a7017",
        "23c0813d5cb4e4d6",
        "55db7704-2c15-48b5-a0a6-e1047ca90fdf",
        "e6429ad5-a41c-4e9f-9548-8c3063e4be44"
    };

    public static bool InBlacklist(this string deviceId)
    {
        return Blacklist.Contains(deviceId);
    }
}