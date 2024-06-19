using Newtonsoft.Json;

namespace RGB4R.RazerChroma;

/// <summary>
/// The author information required during initialization.
/// </summary>
public class InitAuthor
{
    /// <summary>
    /// The name of the author.
    /// </summary>
    /// <remarks>
    /// Maximum allowed length is 64 characters.
    /// </remarks>
    [JsonProperty("name")]
    public string Name { get; set; }
    /// <summary>
    /// A contact URL or Address for the application.
    /// </summary>
    /// <remarks>
    /// Maximum allowed length is 64 characters.
    /// </remarks>
    [JsonProperty("contact")]
    public string Contact { get; set; }
}
