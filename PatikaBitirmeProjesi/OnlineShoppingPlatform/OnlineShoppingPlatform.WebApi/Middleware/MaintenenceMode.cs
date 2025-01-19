using OnlineShoppingPlatform.Business.Operations.Settings;

namespace OnlineShoppingPlatform.WebApi.Middleware
{
    public class MaintenenceMode
    {

        //bakım işlemlerini yapıyorum.

        private readonly RequestDelegate _requestDelegate;
       

        public MaintenenceMode(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = settingService.GetMaintenenceMode();

            if(context.Request.Path.StartsWithSegments("/api/auth/login") ||
                context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _requestDelegate(context);

                return;
            }


            if (maintenanceMode)
            {
                await context.Response.WriteAsync("Şu anda sitemiz bakım durumunda. En kısa sürede çözülecektir.");
            }

            else
            {
                await _requestDelegate(context);
            }
        }
    }
}
