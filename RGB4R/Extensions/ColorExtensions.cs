using System.Drawing;

namespace RGB4R.Extensions;

/// <summary>
/// Extensions for converting colors between different types.
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Converts a <see cref="Color"/> to BGR.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>The color converted to BGR as an integer.</returns>
    public static int ToBgr(this Color color)
    {
        return (color.B << 16) + (color.G << 8) + color.R;
    }
}
