using AutoMapper;
using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Requests;
using Battleship_Royal.Data.Entities.Identity;

namespace Battleship_Royal.Api.Mappings
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<ApplicationUser, NewUserRegistrationRequest>()
                .ForMember(x =>  x.NickName, x => x.MapFrom(y => y.Nickname));
            CreateMap<NewUserRegistrationRequest, NewUserDto>();


        }

    }
}
