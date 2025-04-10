namespace UserManagementService.BusinessLogicLayer.Exceptions.Jwt
{
    class InvalidJwtExpiryException : JwtConfigurationException
    {
        public InvalidJwtExpiryException()
        : base("JWT ExpiryMinutes должны быть позитивны")
        { }
    }
}
