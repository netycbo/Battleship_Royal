using Battleship_Royal.Api.Responses;
using Battleship_Royal.Api.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Battleship_Royal.Api.Requests
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
        [DataType(DataType.Password),Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [NoSpecialCharactersAttribute]
        public string NickName { get; set; }
        public string Role { get; set; }
    }
}
