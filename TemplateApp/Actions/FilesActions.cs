using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using TemplateApp.Invocables;
using TemplateApp.Models.Request;
using TemplateApp.Models.Response;

namespace TemplateApp.Actions;

/// <summary>
/// Contains list of file actions
/// </summary>
[ActionList]
public class FilesActions(InvocationContext invocationContext, IFileManagementClient _fileManagementClient) : AppInvocable(invocationContext)
{
    // Injecting instance of IFileManagementClient that helps to work with files inside of Blackbird
    
    /// <summary>
    /// Demonstration of downloading file with IFileManagementClient
    /// </summary>
    /// <param name="input">Action parameter with the data for downloading the file</param>
    /// <returns>File data</returns>
    [Action("Download file", Description = "Downloads a file from the supplied URL.")]
    public async Task<FileResponse> DownloadFile([ActionParameter] DownloadFileRequest input)
    {
        var request = new RestRequest(input.FileUrl);
        var response = await Client.ExecuteAsync(request);

        // Throwing error if status code is not successful
        if (!response.IsSuccessStatusCode)
            throw new PluginApplicationException($"Could not download your file; Code: {response.StatusCode}");

        // Uploading downloaded file to Blackbird
        var file = await _fileManagementClient.UploadAsync(new MemoryStream(response.RawBytes!), response.ContentType!,
            input.FileName);
        return new(file);
    }

    /// <summary>
    /// Demonstration of downloading a large file with IFileManagementClient
    /// </summary>
    /// <param name="input">Action parameter with the data for downloading the file</param>
    /// <returns>File data</returns>
    [Action("Download large file", Description = "Downloads a large file from the supplied URL while keeping memory use low.")]
    public async Task<FileResponse> DownloadLargeFile([ActionParameter] DownloadFileRequest input)
    {
        // In this case the Blackbird core will download the file efficiently, without a strain on time or memory.
        var request = new HttpRequestMessage(HttpMethod.Get, input.FileUrl);
        var file = new FileReference(request, input.FileName, "text/html");
        return new(file);
    }

    /// <summary>
    /// Demonstration of uploading file with IFileManagementClient
    /// </summary>
    /// <param name="input">Action parameter with the data for downloading the file</param>
    /// <returns>File data</returns>
    [Action("Upload file", Description = "Uploads a file to the supplied URL.")]
    public async Task UploadFile([ActionParameter] UploadFileRequest input)
    {
        // Downloading input file from Blackbird into a stream
        var fileStream = await _fileManagementClient.DownloadAsync(input.File);
        
        var request = new RestRequest(input.UploadUrl, Method.Post)
            .AddFile("file", () => fileStream, input.File.Name);
        var response = await Client.ExecuteAsync(request);

        // Throwing error if status code is not successful
        if (!response.IsSuccessStatusCode)
            throw new PluginApplicationException($"Could not upload your file; Code: {response.StatusCode}");
    } 
}