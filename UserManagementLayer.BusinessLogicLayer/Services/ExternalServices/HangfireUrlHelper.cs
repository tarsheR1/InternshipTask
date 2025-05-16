using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;

namespace UserManagementService.BusinessLogicLayer.Services.ExternalServices
{
    public class HangfireUrlHelper : IHangfireUrlHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IConfiguration _config;

        public HangfireUrlHelper(LinkGenerator linkGenerator, IConfiguration config)
        {
            _linkGenerator = linkGenerator;
            _config = config;
        }

        public string GenerateAbsoluteUrl(string action, string controller, object values)
        {
            var baseUrl = _config["AppSettings:BaseUrl"];

            var url = _linkGenerator.GetPathByAction(action, controller, values);
            return $"{baseUrl}{url}";
        }
    }
}
