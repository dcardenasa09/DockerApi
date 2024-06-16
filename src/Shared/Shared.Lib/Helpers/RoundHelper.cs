namespace Shared.Lib.Helpers;

public static class RoundHelper {
    public static double Round(this double number, int numberDecimals) {
        return Math.Round(number, numberDecimals);
    }

    public static decimal Round(this decimal number, int numberDecimals) {
        return Decimal.Round(number, numberDecimals);
    }
}