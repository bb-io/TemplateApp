using Blackbird.Applications.Sdk.Common.Invocation;
using TemplateApp.Events.Handlers.Base;

namespace TemplateApp.Events.Handlers;

/// <summary>
/// Handler for item.created webhook
/// </summary>
public class ItemCreatedHandler(InvocationContext context) : ParameterlessWebhookHandler(context)
{
    protected override string SubscriptionEvent => "item.created";
}