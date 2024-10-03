using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Dtos.Game;
using Battleship_Royal.Api.Requests.Game;
using Battleship_Royal.Api.Requests.Players;
using Battleship_Royal.Data.Entities;
using Battleship_Royal.Data.Entities.Identity;

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
            CreateMap<ApplicationUser, LoginDto>();
            CreateMap<ApplicationUser, NewUserDto>()
                .ForMember(x => x.NickName, x => x.MapFrom(y=> y.UserName));
            CreateMap<PrepareGameRequest, PrepareGameDto>();
            CreateMap<PrepareGameDto, TemporaryGame>()
                .ForMember(dest => dest.Player1Id, opt => opt.MapFrom(src => src.Player1))
                .ForMember(dest => dest.Player2Id, opt => opt.MapFrom(src => src.Player2));


        }

    }
}
