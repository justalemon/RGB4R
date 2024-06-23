using System;

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
    public Guid Id { get; protected set; }
    /// <summary>
    /// Whether the effect is registered or not.
    /// </summary>
    public bool IsRegistered => Id != Guid.Empty;

    #endregion
    
    #region Functions
    
    /// <summary>
    /// Registers this effect with Razer.
    /// </summary>
    public abstract void Register();

    #endregion
}
