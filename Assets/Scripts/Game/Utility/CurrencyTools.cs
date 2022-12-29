using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public static class CurrencyTools
{
    private static readonly IDictionary<string, string> Map;

    static CurrencyTools()
    {
        Map = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture =>
            {
                try
                {
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri != null)
            .GroupBy(ri => ri.ISOCurrencySymbol)
            .ToDictionary(x => x.Key, x => x.First().CurrencySymbol);
    }

    public static bool TryGetCurrencySymbol(string isoCurrencySymbol, out string symbol)
    {
        return Map.TryGetValue(isoCurrencySymbol, out symbol);
    }
}