using IT_Talent_Program.Helpers;

namespace IT_Talent_Program.Dtos
{
    public class RegisterDto
    {
        [Custom("Login")]
        public string Login { get; set; }
        [Custom("Password")]
        public string Password { get; set; }
        [Name]
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Admin { get; set; }
    }
}
