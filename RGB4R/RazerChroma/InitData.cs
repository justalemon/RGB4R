using System.Collections.Generic;
using Newtonsoft.Json;

namespace RGB4R.RazerChroma;

/// <summary>
/// The information required to initialize the Razer Chroma connection.
/// </summary>
public class InitData
{
    /// <summary>
    /// The Title of the application.
    /// </summary>
    /// <remarks>
    /// Maximum allowed length is 64 characters.
    /// </remarks>
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>
    /// The description of the application.
    /// </summary>
    /// <remarks>
    /// Maximum allowed length is 256 characters.
    /// </remarks>
    [JsonProperty("description")]
    public string Description { get; set; }
    /// <summary>
    /// The author of the application.
    /// </summary>
    [JsonProperty("author")]
    public InitAuthor Author { get; set; }
    /// <summary>
    /// The devices that are supported by the application.
    /// </summary>
    /// <remarks>
    /// The supported values are keyboard/mouse/headset/mousepad/keypad/chromalink.
    /// </remarks>
    [JsonProperty("device_supported")]
    public List<string> DevicesSupported { get; set; } = [];
    /// <summary>
    /// The category of the application.
    /// </summary>
    /// <remarks>
    /// This is either application or game.
    /// </remarks>
    [JsonProperty("category")]
    public string Category { get; set; } = "game";
}
