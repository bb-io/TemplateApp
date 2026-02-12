using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using TemplateApp.Constants;

namespace TemplateApp.Connections;

/// <summary>
/// Describes BlackBird's app connection settings
/// </summary>
public class ConnectionDefinition : IConnectionDefinition
{
    /// <summary>
    /// Defines app's connection types
    /// </summary>
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        // API token auth example
        new()
        {
            Name = "Credentials",
            AuthenticationType = ConnectionAuthenticationType.Undefined,

            // Specifying properties that we will need for authorization of the app
            ConnectionProperties =
            [
                new(CredsNames.ApiToken)
                {
                    // Property user-friendly name that will be displayed on the UI
                    DisplayName = "API token",

                    // Setting this flag to true hides token input, replacing each its character with •
                    Sensitive = true,
                    // Description of the connection property,
                    // perhaps with some guidelines on how to find it in the service
                    Description = "You can create API token in your profile settings, on the API tab"
                },
                new(CredsNames.ApiType)
                {
                    // Property user-friendly name that will be displayed on the UI
                    DisplayName = "API version",

                    // Setting this flag to false does not hide the input
                    Sensitive = false,
                    // Description of the connection property,
                    // perhaps with some guidelines on how to find it in the service
                    Description = "API version",
                    // Static possible inputs for the connection property.
                    // Array of objects where first element is a value needed for the API and second element is a user-friendly name.
                    DataItems = [new("v1", "Version 1"), new("v2", "Version 2")]
                }
            ]
        },

        // OAuth example. An App can have multiple different connections and types
        new()
        {
            Name = "OAuth",
            AuthenticationType = ConnectionAuthenticationType.OAuth2,
            ConnectionProperties =
            [
                new(CredsNames.Url)
                {
                    DisplayName = "URL",
                    Sensitive = false
                }
            ]
        }
    };


    /// <summary>
    /// Processes credentials after the authorization is done 
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var providers = new List<AuthenticationCredentialsProvider>();

        string? token = values.FirstOrDefault(v => v.Key == CredsNames.AccessToken).Value;
        if (token != null)
            providers.Add(new("Authorization", $"Bearer {token}"));

        foreach (var kv in values.Where(x => x.Key != CredsNames.AccessToken))
            providers.Add(new AuthenticationCredentialsProvider(kv.Key, kv.Value));

        // Optionally you can also add extra variables such as the connection type
        
        return providers;
    }
}