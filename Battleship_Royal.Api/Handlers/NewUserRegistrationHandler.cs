using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Handlers
{
    public class NewUserRegistrationHandler(IMapper mapper, UserManager<ApplicationUser> userManager) : IRequestHandler<NewUserRegistrationRequest, NewUserRegistrationResponse>
    {
        public async Task<NewUserRegistrationResponse> Handle(NewUserRegistrationRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var user = mapper.Map<ApplicationUser>(request);
            var creationResult = await userManager.CreateAsync(user, request.Password);
            if (!creationResult.Succeeded)
            {
                throw new Exception("User creation failed: " + string.Join(", ", creationResult.Errors.Select(e => e.Description)));
            }

            var roleAssignmentResult = await userManager.AddToRoleAsync(user, "Player");
            if (!roleAssignmentResult.Succeeded)
            {
                throw new Exception("Role assignment failed: " + string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description)));
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var newUserDto = mapper.Map<NewUserDto>(user);
            newUserDto.RoleName = string.Join(", ", userRoles);

            return new NewUserRegistrationResponse
            {
                Data = newUserDto
            };
        }
    }

}
