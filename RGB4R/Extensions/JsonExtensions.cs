﻿using System.Threading.Tasks;
using Flurl.Http;
using GTA;

namespace RGB4R.Extensions;

/// <summary>
/// Extensions to get the JSON of Flurl Requests Synchronously.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Gets the JSON data of a response synchronously.
    /// </summary>
    /// <param name="response">The response information.</param>
    /// <typeparam name="T">The type to get.</typeparam>
    /// <returns>The type parse from the JSON.</returns>
    public static T GetJsonSync<T>(this IFlurlResponse response)
    {
        Task<T> request = Task.Run(response.GetJsonAsync<T>);
        while (!request.IsCompleted)
        {
            Script.Yield();
        }
        return request.Result;
    }
}