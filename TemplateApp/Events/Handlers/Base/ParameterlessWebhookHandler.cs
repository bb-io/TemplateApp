using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;
using TemplateApp.Constants;
using TemplateApp.Events.Models.Request;
using TemplateApp.Invocables;

namespace TemplateApp.Events.Handlers.Base;

/// <summary>
/// Base handler for parameterless webhooks
/// </summary>
public abstract class ParameterlessWebhookHandler(InvocationContext invocationContext) : AppInvocable(invocationContext), IWebhookEventHandler
{
    protected abstract string SubscriptionEvent { get; }

    /// <summary>
    /// Subscribes to a webhook event
    /// </summary>
    public Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var request = new RestRequest(ApiEndpoints.Webhooks, Method.Post);
        request.AddJsonBody(new AddWebhookRequest
        {
            // Webhook bird url that will receive webhook data
            Url = values["payloadUrl"],
            Event = SubscriptionEvent
        });

        return Client.ExecuteWithHandling(request);
    }

    /// <summary>
    /// Unsubscribes from a webhook event
    /// </summary>
    public Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var request = new RestRequest(ApiEndpoints.Webhooks, Method.Delete);
        request.AddJsonBody(new AddWebhookRequest
        {
            Url = values["payloadUrl"],
            Event = SubscriptionEvent
        });

        return Client.ExecuteWithHandling(request);
    }
}