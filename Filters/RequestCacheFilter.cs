using Microsoft.AspNetCore.Mvc.Filters;

namespace wallets_api_wrapper.Filters
{
    public class RequestCacheFilter: ActionFilterAttribute
    {
        public int CacheDuration { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // context.HttpContext.
        }

        
    }
}