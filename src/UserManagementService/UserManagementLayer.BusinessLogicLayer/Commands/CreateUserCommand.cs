namespace UserManagementService.BusinessLogicLayer.Commands
{
    public sealed record CreateUserCommand
    {
        public string Email { get; init; }
        public string PasswordHash { get; init; }  
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? MiddleName { get; init; }
        public string? Phone { get; init; }

        public CreateUserCommand(
            string email,
            string passwordHash,  
            string firstName,
            string lastName,
            string? middleName = null,
            string? phone = null)
        {
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Phone = phone;
        }
    }
}
