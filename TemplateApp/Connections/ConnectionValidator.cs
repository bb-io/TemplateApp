using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using TemplateApp.Constants;
using TemplateApp.Invocables;

namespace TemplateApp.Connections;

/// <summary>
/// Validates app credentials that were provided by the user
/// </summary>
public class ConnectionValidator(InvocationContext invocationContext) : AppInvocable(invocationContext), IConnectionValidator
{    
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
    {
        var client = new AppRestClient(authProviders);
        var request = new RestRequest(ApiEndpoints.Berry, Method.Get);

        try
        {
            var response = await Client.ExecuteAsync(request);

            // Typically you'll want to use the least complex way to validate if a connection is valid.
            var isValid = response.StatusCode != System.Net.HttpStatusCode.Unauthorized;

            return new ConnectionValidationResponse
            {
                IsValid = isValid,
                Message = isValid ? "Success" : (response.Content ?? response.ErrorMessage ?? response.StatusCode.ToString()),
            };
        }
        catch (Exception ex)
        {
            // If Exception was thrown, we mark connection credentials as invalid
            // and pass the exception message in the response
            // We also log the error

            InvocationContext.Logger?.LogError($"Connection validation failed: {ex.Message}", []);

            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}