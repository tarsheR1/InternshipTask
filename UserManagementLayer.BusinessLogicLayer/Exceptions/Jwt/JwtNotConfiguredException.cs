using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    class JwtNotConfiguredException : JwtConfigurationException
    {
        public JwtNotConfiguredException()
        : base("JWT настроен некорректно")
        { }
    }
}
