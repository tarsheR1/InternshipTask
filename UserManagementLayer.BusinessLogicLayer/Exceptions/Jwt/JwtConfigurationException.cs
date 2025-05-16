using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    public class JwtConfigurationException : BusinessLogicException
    {
        public JwtConfigurationException(string message)
            : base("jwt_config_error",
                  message) 
        { }
    }
}
