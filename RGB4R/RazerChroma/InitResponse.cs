using Newtonsoft.Json;

namespace RGB4R.RazerChroma;

/// <summary>
/// The response after initialization.
/// </summary>
public class InitResponse
{
    /// <summary>
    /// The ID of the Session
    /// </summary>
    [JsonProperty("sessionid")]
    public int SessionId { get; set; }
    /// <summary>
    /// The URI that corresponds to the session.
    /// </summary>
    [JsonProperty("uri")]
    public string Uri { get; set; }
    /// <summary>
    /// The error message, if any.
    /// </summary>
    [JsonProperty("error")]
    public string ErrorMessage { get; set; }
    /// <summary>
    /// The result error, if any.
    /// </summary>
    [JsonProperty("result")]
    public RazerError ErrorCode { get; set; }
}
