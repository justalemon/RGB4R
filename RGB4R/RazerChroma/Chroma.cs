﻿using System;
using Flurl;
using Flurl.Http;
using GTA;
using RGB4R.Extensions;

namespace RGB4R.RazerChroma;

/// <summary>
/// Class for handling and processing the connection for operating Razer Chroma devices via Razer Synapse 3.
/// </summary>
public static class Chroma
{
    #region Fields

    private const string registrationUri = "http://localhost:54235/razer/chromasdk";
    private static int lastHeartbeat = -1;

    #endregion

    #region Properties

    /// <summary>
    /// Whether the Razer Synapse subsystem is ready to work.
    /// </summary>
    public static bool IsReady => Endpoint != null;
    /// <summary>
    /// The currently assigned endpoint.
    /// </summary>
    public static Url Endpoint { get; private set; }

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

        IFlurlResponse flurlResponse = registrationUri.PostJsonAsync(initData).Yield();
        InitResponse response = flurlResponse.GetJsonAsync<InitResponse>().Yield();

        if (response.ErrorCode != RazerError.Success)
        {
            throw new RazerException(response.ErrorMessage, response.ErrorCode);
        }

        Endpoint = response.Uri;
        lastHeartbeat = 0;
    }
    /// <summary>
    /// Sends a heartbeat to the Razer Ports.
    /// </summary>
    public static void PerformHeartbeat()
    {
        if (IsReady && lastHeartbeat + 1000 <= Game.GameTime)
        {
            try
            {
                Endpoint.Clone().AppendPathSegment("heartbeat").PutAsync().Yield();
            }
            catch (FlurlHttpException)
            {
                Endpoint = null;
            }

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
            Endpoint.DeleteAsync().Yield();
            Endpoint = null;
        }
    }

    #endregion
}
