using SimplePainterServer.Dto;

namespace Painter
{
    public class WordInfoDetail : WordInfoDto
    {
        public WordInfoDetail(int id, string name, string partSpeech, int drawCount, int? userId) : base(
            id, name, partSpeech, userId) =>
            DrawCount = drawCount;

        public int DrawCount { get; set; }
    }
}