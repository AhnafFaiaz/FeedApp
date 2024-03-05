namespace FeedApp.UI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                // Redirect to the Signin page if the user is not authenticated
                context.Response.Redirect("/Signin/Signin");
                return;
            }

            await _next(context);
        }
    }
}
