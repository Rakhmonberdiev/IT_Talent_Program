﻿using IT_Talent_Program.Helpers;

namespace IT_Talent_Program.Dtos
{
    public class LoginRequestDto
    {
        [Custom("Login")]
        public string Login {  get; set; }
        [Custom("Password")]
        public string Password { get; set; }
    }
}
