using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using TemplateApp.Constants;
using TemplateApp.Invocables;
using TemplateApp.Models.Dto;
using TemplateApp.Models.Response;

namespace TemplateApp.DataSourceHandlers;

/// <summary>
/// Data source handler for asynchronous dynamic inputs.
/// It provides data on the UI, so that user can choose values from the dropdown instead of inputting it manually.
/// </summary>
public class AsyncDataSourceHandler(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    /// <summary>
    /// Fetches data for the dynamic inputs and returns it as a list of options.
    /// </summary>
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var request = new RestRequest(ApiEndpoints.Berry, Method.Get);
        var response = await Client.ExecuteWithHandling<ListResponse<Berry>>(request);

        return response.Results
            // We need to pay attention to SearchString in the context
            // So that we return only values that match user search request
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.Name));
    }
}