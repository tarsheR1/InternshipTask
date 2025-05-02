using UserManagementService.PresentationLayer.Extensions.Middleware;

namespace UserManagementService.PresentationLayer.Extensions.Application
{
    public static class ApplicationExtension
    {
        public static void ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGlobalErrorHandling();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
