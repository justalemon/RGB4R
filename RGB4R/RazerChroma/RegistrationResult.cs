using System;
using Newtonsoft.Json;

namespace RGB4R.RazerChroma;

/// <summary>
/// The data sent back after registering an effect.
/// </summary>
public class RegistrationResult
{
    /// <summary>
    /// A message related to the color.
    /// </summary>
    [JsonProperty("color")]
    public string Color { get; set; }
    /// <summary>
    /// Any error code returned by the call.
    /// </summary>
    [JsonProperty("result")]
    public RazerError Error { get; set; }
    /// <summary>
    /// The ID returned, if successful.
    /// </summary>
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
