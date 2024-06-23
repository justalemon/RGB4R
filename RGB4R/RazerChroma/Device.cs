namespace RGB4R.RazerChroma;

/// <summary>
/// The different types of devices allowed in Razer Chroma.
/// </summary>
public enum Device
{
    /// <summary>
    /// Keyboards.
    /// </summary>
    Keyboard,
    /// <summary>
    /// Mouse, internally called Mice.
    /// </summary>
    Mouse,
    /// <summary>
    /// Headsets and Headphones.
    /// </summary>
    Headset,
    /// <summary>
    /// Mousepads.
    /// </summary>
    Mousepad,
    /// <summary>
    /// Macropads, internally called Keypads.
    /// </summary>
    Keypad,
    /// <summary>
    /// Devices connected via Chroma Link.
    /// </summary>
    ChromaLink
}
