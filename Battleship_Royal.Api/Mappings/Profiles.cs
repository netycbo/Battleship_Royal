using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Api.Responses.Players;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Battleship_Royal.Api.Mappings
{
    public class Profiles: Profile  
    {
        
        public Profiles()
        {
            CreateMap<ApplicationUser, NewUserRegistrationRequest>()
                .ForMember(x => x.NickName, x => x.MapFrom(y => y.UserName));
            CreateMap<NewUserRegistrationRequest, ApplicationUser>()
                .ForMember(x => x.UserName, x => x.MapFrom(y => y.NickName));
            CreateMap<NewUserRegistrationRequest, NewUserDto>();
            CreateMap<NewUserRegistrationResponse,NewUserDto>();
                
                
            CreateMap<ApplicationUser, LoginDto>();
            CreateMap<ApplicationUser, NewUserDto>()
                .ForMember(x => x.NickName, x => x.MapFrom(y=> y.UserName));
            CreateMap<PrepareGameVsComputerRequest, PrepareGameVsComputerDto>();
            CreateMap<SaveToDbRequest, SaveToDbDto>();
            CreateMap<SaveToDbDto, Game>();
            CreateMap<PrepareGameVsComputerRequest, SaveToDbWithComputerPlayerDto>();
            CreateMap<RefreshTokenRequest, RefreshTokenDto>();
        }

    }
}
