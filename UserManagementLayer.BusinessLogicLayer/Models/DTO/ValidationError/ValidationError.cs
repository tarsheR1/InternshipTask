namespace UserManagementService.BusinessLogicLayer.Models.DTO.ValidationError
{
    public class ValidationError
    {
        public string Code { get; }
        public string Message { get; }
        public string PropertyName { get; }

        public ValidationError(string code, string message, string propertyName = null)
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
        }
    }

}
