namespace UserManagementService.BusinessLogicLayer.Models.DTO.Request.Users
{
    public sealed record UpdateUserRequestDto
    {
        public string? Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? MiddleName { get; init; }
        public string? Phone { get; init; }
        public string? EmailActivationToken { get; init; }
    }
}
