using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Handlers
{
    public class NewUserRegistrationHandler(IMapper mapper, IMediator mediator,
        UserManager<ApplicationUser> userManager) : IRequestHandler<NewUserRegistrationRequest, NewUserRegistrationResponse>
    {
        public async Task<NewUserRegistrationResponse> Handle(NewUserRegistrationRequest request, CancellationToken cancellationToken)
        {
            var userExist = await userManager.FindByNameAsync(request.Email);

            if (userExist != null)
            {
               throw new Exception("User already exist");
            }
            var EmailExist = await userManager.FindByEmailAsync(request.Email);
            if (EmailExist != null) 
            {
                throw new Exception("Email already exist");
            }
            var user = mapper.Map<ApplicationUser>(request);
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User creation failed");
            }

            var roleAssighment = await userManager.AddToRoleAsync(user, "Player");
            if (!roleAssighment.Succeeded)
            {
                throw new Exception("Role assignment failed");
            }
            return new NewUserRegistrationResponse
            {
                Data = mapper.Map<NewUserDto>(user)
            };
        }
    }
}
