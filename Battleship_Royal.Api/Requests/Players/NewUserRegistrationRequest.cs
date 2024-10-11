using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Api.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests.Players
{
    public class NewUserRegistrationRequest : IRequest<NewUserRegistrationResponse>
    {
        [Required]
        [DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} znaki.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane.")]
        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nick jest wymagany.")]
        [NoSpecialCharacters]
        public string NickName { get; set; } = string.Empty;
        
    }
}
