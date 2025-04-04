namespace TicketManagementService.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (TicketsSoldOutException ex)
            {
                context.Response.StatusCode = 409; 
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
            }
            catch (TicketInventoryNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
            }
        }
    }
}
