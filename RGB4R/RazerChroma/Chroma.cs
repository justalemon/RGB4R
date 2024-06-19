using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GTA;

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

        Task<IFlurlResponse> taskPost = Task.Run(() => uri.PostJsonAsync(initData));
        while (!taskPost.IsCompleted)
        {
            Script.Yield();
        }
        IFlurlResponse resp = taskPost.Result;

        Task<InitResponse> taskText = Task.Run(() => resp.GetJsonAsync<InitResponse>());
        while (!taskText.IsCompleted)
        {
            Script.Yield();
        }
        InitResponse response = taskText.Result;

        if (response.ErrorCode != RazerError.Success)
        {
            throw new RazerException(response.ErrorMessage, response.ErrorCode);
        }

        IsReady = true;
        uri = response.Uri;
    }
    /// <summary>
    /// Sends a heartbeat to the Razer Ports.
    /// </summary>
    public static void PerformHeartbeat()
    {
        if (!IsReady)
        {
            return;
        }
        
        if (lastHeartbeat + 1000 <= Game.GameTime)
        {
            Task.Run(() => uri.AppendPathSegment("heartbeat").PutAsync());
            lastHeartbeat = Game.GameTime;
        }
    }
    /// <summary>
    /// Uninitializes the Razer Chroma connection.
    /// </summary>
    public static void Uninitialize()
    {
        if (!IsReady) return;

        Task task = Task.Run(() => uri.DeleteAsync());
        while (!task.IsCompleted)
        {
            Script.Yield();
        }
    }

    #endregion
}
