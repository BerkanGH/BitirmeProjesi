using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShoppingPlatform.WebApi.Filters
{
    public class TimeFilter : ActionFilterAttribute

    {
        public string StarTime { get; set; }

        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            var now = DateTime.Now.TimeOfDay;

            StarTime = "20:00";

            EndTime =  "21:00";

            if (now >= TimeSpan.Parse(StarTime) && now <= TimeSpan.Parse(EndTime))
            {

                base.OnActionExecuting(context);
            }

            else
            {
                context.Result = new ContentResult
                {
                    Content = "bu saatlar arasında çalışmaya kapalıdır",
                    StatusCode = 403
                };
            }
        }

    }
}
