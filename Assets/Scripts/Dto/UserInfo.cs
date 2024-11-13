namespace SimplePainterServer.Dto
{
    public class UserInfoDto
    {
        public UserInfoDto(int    id, string name, string language, string sex, string age, string career,
                           string educationLevel)
        {
            ID             = id;
            Name           = name;
            Language       = language;
            Sex            = sex;
            Age            = age;
            Career         = career;
            EducationLevel = educationLevel;
        }

        public int    ID             { get; set; }
        public string Name           { get; set; }
        public string Language       { get; set; }
        public string Sex            { get; set; }
        public string Age            { get; set; }
        public string Career         { get; set; }
        public string EducationLevel { get; set; }
    }
}