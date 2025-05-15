namespace UserManagementService.BusinessLogicLayer.Models.DTO.Response
{
    public class RegisterUserResponseDto
    {
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
