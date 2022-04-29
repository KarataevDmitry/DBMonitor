using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DBMonitor.UI.Controllers
{
#if DEBUG
    [Route("[controller]")]
    public class EnvironmentController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public EnvironmentController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet("routes", Name = "ApiEnvironmentGetAllRoutes")]
        public IActionResult GetAllRoutes()
        {
            /* intentional use of var/anonymous class since this method is purely informational */
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
                .Where(ad => ad.AttributeRouteInfo != null)
                .Select(x => new
                {
                    Action = null != x && null != x.RouteValues && null != x.RouteValues["action"] ? x.RouteValues["action"] : "n/a",
                    Controller = null != x && null != x.RouteValues && null != x.RouteValues["controller"] ? x.RouteValues["controller"] : "n/a",
                    Name = x.AttributeRouteInfo.Name ?? "n/a",
                    Template = x.AttributeRouteInfo.Template ?? "n/a",
                    Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First()
                }).ToList();
            return Ok(routes);
        }
    }
#endif
}
