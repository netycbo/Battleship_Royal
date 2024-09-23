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
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [NoSpecialCharacters]
        public string NickName { get; set; }
        
    }
}
