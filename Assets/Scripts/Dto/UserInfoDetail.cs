using SimplePainterServer.Dto;

namespace Painter
{
    public class UserInfoDetail : UserInfoDto
    {
        public UserInfoDetail(int id,
            string                name,
            string                language,
            string                sex,
            string                age,
            string                career,
            string                educationLevel) : base(id, name, language, sex, age, career, educationLevel)
        {
        }

        public int DrawCount         { get; set; }
        public int GuessCount        { get; set; }
        public int GuessSuccessCount { get; set; }
        
    }
}