using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using TemplateApp.Constants;

namespace TemplateApp;

public class AppRestClient : RestClient
{
    private readonly IEnumerable<AuthenticationCredentialsProvider> _credsProviders;

    public AppRestClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(new RestClientOptions { BaseUrl = new(Urls.Api) })
    {
        _credsProviders = authenticationCredentialsProviders;
        var authHeader = _credsProviders.First(x => x.KeyName == CredsNames.ApiToken);
        this.AddDefaultHeader(authHeader.KeyName, authHeader.Value);
    }

    public async Task<T> ExecuteWithHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithHandling(request);
        return JsonConvert.DeserializeObject<T>(response.Content!)!;
    }

    public async Task<RestResponse> ExecuteWithHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
            return response;
        
        throw new PluginApplicationException(response.Content ?? response.ErrorMessage ?? $"Application responded with error {response.StatusCode}");
    }
}