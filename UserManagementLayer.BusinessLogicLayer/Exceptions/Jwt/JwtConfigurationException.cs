using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.BusinessLogicLayer.Exceptions.Core;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    class JwtConfigurationException : BusinessLogicException
    {
        public JwtConfigurationException(string message)
            : base("jwt_config_error",
                  message,
                  500) 
        { }
    }
}
