using IT_Talent_Program.Helpers;

namespace IT_Talent_Program.Dtos
{
    public class LoginUpdateDto
    {
        [Custom("Login")]
        public string Login { get; set; }
        [Custom("New login")]
        public string NewLogin { get; set; }
    }
}
