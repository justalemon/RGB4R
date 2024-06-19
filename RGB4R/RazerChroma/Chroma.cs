using Flurl;
using Flurl.Http;
using GTA;
using Newtonsoft.Json.Linq;
using RGB4R.Extensions;

namespace RGB4R.RazerChroma;

/// <summary>
/// Class for handling and processing the connection for operating Razer Chroma devices via Razer Synapse 3.
/// </summary>
public static class Chroma
{
    #region Fields

    private static string uri = "http://localhost:54235/razer/chromasdk";
    private static int lastHeartbeat = -1;
    
    #endregion
    
    #region Properties

    /// <summary>
    /// Whether the Razer Synapse subsystem is ready to work.
    /// </summary>
    public static bool IsReady { get; private set; }
    /// <summary>
    /// The current Session ID.
    /// </summary>
    public static int SessionId { get; private set; }

    #endregion
    
    #region Tools

    private static JObject EffectToJson(Effect effect)
    {
        JObject obj = new JObject();

        if (effect is EffectStatic staticEffect)
        {
            obj["effect"] = "CHROMA_STATIC";
            obj["param"] = new JObject();
            obj["param"]["color"] = staticEffect.Color.ToArgb();
        }

        return obj;
    }

    #endregion

    #region Functions

    /// <summary>
    /// Initializes Razer Synapse.
    /// </summary>
    public static void Initialize()
    {
        InitData initData = new InitData
        {
            Title = "RGB4R",
            Description = "Mod that adds RGB support for Grand Theft Auto V",
            Author = new InitAuthor
            {
                Name = "Hannele \"Lemon\" Ruiz",
                Contact = "https://discord.gg/Cf6sspj"
            },
            Category = "game",
            DevicesSupported = [
                "keyboard",
                "mouse",
                "headset",
                "mousepad",
                "keypad"
            ],
        };

        IFlurlResponse flurlResponse = uri.PostJsonSync(initData);
        InitResponse response = flurlResponse.GetJsonSync<InitResponse>();

        if (response.ErrorCode != RazerError.Success)
        {
            throw new RazerException(response.ErrorMessage, response.ErrorCode);
        }

        uri = response.Uri;
        IsReady = true;
    }
    /// <summary>
    /// Sends a heartbeat to the Razer Ports.
    /// </summary>
    public static void PerformHeartbeat()
    {
        if (IsReady && lastHeartbeat + 1000 <= Game.GameTime)
        {
            uri.AppendPathSegment("heartbeat").PutSync();
            lastHeartbeat = Game.GameTime;
        }
    }
    /// <summary>
    /// Uninitializes the Razer Chroma connection.
    /// </summary>
    public static void Uninitialize()
    {
        if (IsReady)
        {
            uri.DeleteSync();
        }
    }

    #endregion
}
