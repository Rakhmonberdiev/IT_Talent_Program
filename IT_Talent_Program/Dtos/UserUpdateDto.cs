namespace IT_Talent_Program.Dtos
{
    public class UserUpdateDto
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
