namespace OnlineShoppingPlatform.WebApi.Middleware
{
    public  static class Middleware
    {

        //program cs de daha az kodla middleware i eklemek için yaptım.
        public static IApplicationBuilder UseMaintenanceMode( this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenenceMode>();

        }
    }
}
