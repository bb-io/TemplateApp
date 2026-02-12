using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using TemplateApp.Events.Handlers.Base;
using TemplateApp.Events.Models.Inputs;

namespace TemplateApp.Events.Handlers;

public class ProjectItemCreatedHandler : ProjectWebhookHandler
{
    protected override string SubscriptionEvent => "project.item.created";

    public ProjectItemCreatedHandler(InvocationContext context, [WebhookParameter] ProjectWebhookInput input) : base(context, input)
    {
    }
}