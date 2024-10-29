using System;

public static class ColorGenerator
{
    public static string GenerateRandomLightColor()
    {
        var random = new Random();
        var red = random.Next(128, 256);
        var green = random.Next(128, 256);
        var blue = random.Next(128, 256);

        return $"#{red:X2}{green:X2}{blue:X2}";
    }
}