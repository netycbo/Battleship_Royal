﻿namespace Battleship_Royal.Api.Dtos
{
    public class NewUserDto
    {
        public string NickName { get; set; }
        public DateOnly JoinDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Role { get; set; }

    }
}