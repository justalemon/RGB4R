using System;
using System.Collections.Generic;

namespace RGB4R.RazerChroma;

/// <summary>
/// Represents an effect that can be sent to a Chroma device.
/// </summary>
public abstract class Effect
{
    #region Properties

    /// <summary>
    /// The ID of this effect, if registered with Razer Chroma.
    /// </summary>
    public Dictionary<Device, Guid> Ids { get; } = [];

    #endregion
    
    #region Functions
    
    /// <summary>
    /// Registers this effect with Razer.
    /// </summary>
    public abstract void Register(Device device);
    /// <summary>
    /// Checks whether a specific device type of the effect is registered.
    /// </summary>
    /// <param name="device">The device type to check.</param>
    public bool IsRegistered(Device device) => Ids.ContainsKey(device);
    /// <summary>
    /// Plays the animation.
    /// </summary>
    /// <param name="devices">The devices to play the animation in.</param>
    public void Play(params Device[] devices)
    {
        foreach (Device device in devices)
        {
            if (!IsRegistered(device))
            {
                Register(device);
            }
        }
        
        // TODO: Implement playback
    }

    #endregion
}
