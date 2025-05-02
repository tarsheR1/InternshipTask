namespace UserManagementService.PresentationLayer.Extensions.Configuration
{
    public static class AuthorizationPolicyExtension
    {
        public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApproveEvent", policy =>
                    policy.RequireClaim("permission", "ApproveEvent"));

                options.AddPolicy("ModerateEvents", policy =>
                    policy.RequireClaim("permission", "ModerateEvents"));

                options.AddPolicy("BuyEventTicket", policy =>
                    policy.RequireClaim("permission", "BuyEventTicket"));

                options.AddPolicy("ProposeEvent", policy =>
                    policy.RequireClaim("permission", "ProposeEvent"));

                options.AddPolicy("ModerateUsers", policy =>
                    policy.RequireClaim("permission", "ModerateUsers"));

                options.AddPolicy("ModerateRoles", policy =>
                    policy.RequireClaim("permission", "ModerateRoles"));

                options.AddPolicy("AssignRoles", policy =>
                    policy.RequireClaim("permission", "AssignRoles"));
            });

            return services;
        }
    }
}
