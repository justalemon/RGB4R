using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GTA;

namespace RGB4R.Extensions;

/// <summary>
/// Extensions to make Flurl requests run synchronously (ish) in SHVDN.
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// Makes a POST request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <param name="data">The data to send.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse PostJsonSync(this string url, object data) => PostJsonSync(new Url(url), data);
    /// <summary>
    /// Makes a POST request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <param name="data">The data to send.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse PostJsonSync(this Url url, object data)
    {
        Task<IFlurlResponse> request = Task.Run(() => url.PostJsonAsync(data));
        while (!request.IsCompleted)
        {
            Script.Yield();
        }
        return request.Result;
    }
    /// <summary>
    /// Makes a PUT request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse PutSync(this string url) => PutSync(new Url(url));
    /// <summary>
    /// Makes a PUT request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse PutSync(this Url url)
    {
        Task<IFlurlResponse> request = Task.Run(() => url.PutAsync());
        while (!request.IsCompleted)
        {
            Script.Yield();
        }
        return request.Result;
    }
    /// <summary>
    /// Makes a DELETE request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse DeleteSync(this string url) => DeleteSync(new Url(url));
    /// <summary>
    /// Makes a DELETE request.
    /// </summary>
    /// <param name="url">The URL to request.</param>
    /// <returns>The result of the response.</returns>
    public static IFlurlResponse DeleteSync(this Url url)
    {
        Task<IFlurlResponse> request = Task.Run(() => url.DeleteAsync());
        while (!request.IsCompleted)
        {
            Script.Yield();
        }
        return request.Result;
    }
}
