﻿using Battleship_Royal.Api.Dtos;
using Battleship_Royal.Api.Requests.Players;

namespace Battleship_Royal.Api.Responses.Players
{
    public class LoginResponse : BaseResponse<LoginDto>
    {
      
        public string Error { get; set; }

    }
}