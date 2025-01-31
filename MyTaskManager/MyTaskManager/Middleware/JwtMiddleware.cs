using MyTaskManager.Services;

namespace MyTaskManager.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
        {
            if (context.Request.Path.StartsWithSegments("/login") || context.Request.Path.StartsWithSegments("/Home/Error") || context.Request.Path.Equals("/"))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Cookies["jwt"];

            if (token != null)
            {
                var user = tokenService.ValidateToken(token);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
                else
                {
                    context.Response.StatusCode = 403;
                    context.Response.Redirect($"/Home/Error/?statusCode={context.Response.StatusCode}");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.Redirect($"/Home/Error/?statusCode={context.Response.StatusCode}");
                return;
            }

            await _next(context);
        }
    }
}
