using System;

namespace RGB4R.RazerChroma;

/// <summary>
/// Exception raised when there is a problem with Razer Synapse.
/// </summary>
public class RazerException : Exception
{
    /// <summary>
    /// Creates a new <see cref="RazerException"/>.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    /// <param name="error">The error code returned by the API.</param>
    public RazerException(string message, RazerError error) : base($"{message}: Code {error} ({(int)error}).")
    {
    }
}
