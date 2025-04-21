namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    public class JwtNotConfiguredException : JwtConfigurationException
    {
        public JwtNotConfiguredException()
        : base("JWT настроен некорректно")
        { }
    }
}
