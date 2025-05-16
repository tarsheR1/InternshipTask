namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    public class InvalidJwtExpiryException : JwtConfigurationException
    {
        public InvalidJwtExpiryException()
        : base("JWT ExpiryMinutes должны быть позитивны")
        { }
    }
}
