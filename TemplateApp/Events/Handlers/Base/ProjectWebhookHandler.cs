using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;
using TemplateApp.Constants;
using TemplateApp.Events.Models.Inputs;
using TemplateApp.Events.Models.Request;
using TemplateApp.Invocables;

namespace TemplateApp.Events.Handlers.Base;

/// <summary>
/// Base handler for webhooks with project ID parameter
/// </summary>
public abstract class ProjectWebhookHandler(InvocationContext invocationContext, [WebhookParameter] ProjectWebhookInput input) : AppInvocable(invocationContext), IWebhookEventHandler
{
    protected abstract string SubscriptionEvent { get; }
    private string ProjectId { get; } = input.ProjectId;

    /// <summary>
    /// Subscribes to a webhook event
    /// </summary>
    public Task SubscribeAsync(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        Dictionary<string, string> values)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{ProjectId}{ApiEndpoints.Webhooks}";
        var request = new RestRequest(endpoint, Method.Post);
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
        var endpoint = $"{ApiEndpoints.Projects}/{ProjectId}{ApiEndpoints.Webhooks}";
        var request = new RestRequest(endpoint, Method.Delete);
        request.AddJsonBody(new AddWebhookRequest
        {
            Url = values["payloadUrl"],
            Event = SubscriptionEvent
        });

        return Client.ExecuteWithHandling(request);
    }
}