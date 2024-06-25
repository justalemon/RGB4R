using System.Drawing;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RGB4R.Extensions;

namespace RGB4R.RazerChroma;

/// <summary>
/// Represents the static effect.
/// </summary>
public class EffectStatic : Effect
{
    #region Properties
    
    /// <summary>
    /// The color to apply.
    /// </summary>
    public Color Color { get; }
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Creates a new static effect.
    /// </summary>
    /// <param name="color">The color to use.</param>
    public EffectStatic(Color color)
    {
        Color = color;
    }
    
    #endregion
    
    #region Functions

    /// <inheritdoc/>
    public override void Register(Device device)
    {
        if (IsRegistered(device))
        {
            return;
        }

        JObject payload = new JObject
        {
            ["effect"] = "CHROMA_STATIC",
            ["param"] = new JObject(),
            ["param"] =
            {
                ["color"] = Color.ToBgr()
            }
        };

        Url url = Chroma.Endpoint.Clone().AppendPathSegment(device.ToString().ToLower());
        IFlurlResponse resp = url.PostJsonAsync(payload).Yield();
        string data = resp.GetStringAsync().Yield();
        Registration result = JsonConvert.DeserializeObject<Registration>(data);

        if (result.Error != RazerError.Success)
        {
            throw new RazerException(result.Color, result.Error);
        }
        
        Ids.Add(device, result.Id);
    }
    
    #endregion
}
