using Blackbird.Applications.Sdk.Common;
using TemplateApp.Models.Dto;

namespace TemplateApp.Events.Models.Payload
{
    public class PollingResponse
    {
        [Display("New berries")]
        public IEnumerable<Berry> NewBerries { get; set; }
    }
}
