namespace SimplePainterServer.Dto
{
    public class GuessDto
    {
        public GuessDto(int id, int imageId, string word, int userId)
        {
            ImageId = imageId;
            Word    = word;
            UserID  = userId;
            ID      = id;
        }

        public int ID { get; set; }

        public int    ImageId   { get; set; }
        public string Word      { get; set; }
        public int    UserID    { get; set; }
        public bool   IsCorrect { get; set; } = false;
    }
}