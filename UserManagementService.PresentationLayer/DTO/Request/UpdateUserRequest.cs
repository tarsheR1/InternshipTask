using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.PresentationLayer.DTO.Request
{
    public sealed record UpdateUserRequest
    {
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [MaxLength(254, ErrorMessage = "Email не должен превышать 254 символа")]
        public string? Email { get; init; }

        [MaxLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string? FirstName { get; init; }

        [MaxLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        public string? LastName { get; init; }

        [MaxLength(50, ErrorMessage = "Отчество не должно превышать 50 символов")]
        public string? MiddleName { get; init; }

        [Phone(ErrorMessage = "Некорректный формат телефона")]
        [MaxLength(20, ErrorMessage = "Телефон не должен превышать 20 символов")]
        public string? Phone { get; init; }
    }
}
