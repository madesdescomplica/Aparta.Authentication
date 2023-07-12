﻿using System.Text;
using System.Text.Json;

namespace Aparta.Authentication.EndToEndTests.Base;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _defaultSerializeOptions;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _defaultSerializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(
        string route,
        object payload
    )
        where TOutput : class
    {

        var payloadJson = JsonSerializer.Serialize(
            payload,
            _defaultSerializeOptions
        );

        var response = await _httpClient.PostAsync(
            route,
            new StringContent(
                payloadJson,
                Encoding.UTF8,
                "application/json"
            )
        );

        var output = await GetOutput<TOutput>(response);

        return (response, output);
    }

    private async Task<TOutput?> GetOutput<TOutput>(HttpResponseMessage response)
        where TOutput : class
    {
        var outputString = await response.Content.ReadAsStringAsync();
        TOutput? output = null;
        if (!string.IsNullOrWhiteSpace(outputString))
            output = JsonSerializer.Deserialize<TOutput>(
                outputString,
                _defaultSerializeOptions
            );
        return output;
    }
}