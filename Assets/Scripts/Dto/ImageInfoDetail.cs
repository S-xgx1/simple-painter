using SimplePainterServer.Dto;

namespace Painter.Dto

{
    public class ImageInfoDetail : ImageInfoDto
    {
        public ImageInfoDetail(int id, int wordId, int userId, int guessCount, int correctCount) : base(id, wordId,
            userId)
        {
        }

        public ImageInfoDetail() : base(0, 0, 0)
        {
        }

        public int    GuessCount   { get; set; }
        public int    CorrectCount { get; set; }
        public string GuessWord    { get; set; }
    }
}