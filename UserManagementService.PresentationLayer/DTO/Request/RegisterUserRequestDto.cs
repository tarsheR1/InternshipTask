namespace UserManagementService.PresentationLayer.DTO.Request
{
    public sealed record RegisterUserRequestDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? MiddleName { get; init; }       
        public string? Phone { get; init; }
    }
}

