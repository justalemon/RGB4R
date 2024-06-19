namespace RGB4R.RazerChroma;

/// <summary>
/// The different errors that the API might return.
/// </summary>
public enum RazerError : long
{
    /// <summary>
    /// Invalid.
    /// </summary>
    Invalid = -1,
    /// <summary>
    /// Success.
    /// </summary>
    Success = 0,
    /// <summary>
    /// Access denied.
    /// </summary>
    AccessDenied = 5,
    /// <summary>
    /// Invalid handle.
    /// </summary>
    InvalidHandle = 6,
    /// <summary>
    /// Not supported.
    /// </summary>
    NotSupported = 50,
    /// <summary>
    /// Invalid parameter.
    /// </summary>
    InvalidParameter = 87,
    /// <summary>
    /// The service has not been started.
    /// </summary>
    ServiceNotActive = 1062,
    /// <summary>
    /// Cannot start more than one instance of the specified program.
    /// </summary>
    SingleInstanceApp = 1152,
    /// <summary>
    /// Device not connected.
    /// </summary>
    DeviceNotConnected = 1167,
    /// <summary>
    /// Element not found.
    /// </summary>
    NotFound = 1168,
    /// <summary>
    /// Request aborted.
    /// </summary>
    Aborted = 1235,
    /// <summary>
    /// An attempt was made to perform an initialization operation when initialization has already been completed.
    /// </summary>
    AlreadyInitialized = 1247,
    /// <summary>
    /// Resource not available or disabled.
    /// </summary>
    Disabled = 4309,
    /// <summary>
    /// Device not available or supported.
    /// </summary>
    DeviceNotAvailable = 4319,
    /// <summary>
    /// The group or resource is not in the correct state to perform the requested operation
    /// </summary>
    NotValidState = 5023,
    /// <summary>
    /// No more items.
    /// </summary>
    NoMoreItems = 259,
    /// <summary>
    /// General failure.
    /// </summary>
    ResultFailed = 2147500037
}
