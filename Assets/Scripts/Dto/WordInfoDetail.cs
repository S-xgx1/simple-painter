using SimplePainterServer.Dto;

namespace Painter
{
    public class WordInfoDetail : WordInfoDto
    {
        public WordInfoDetail(int id, string name, string partSpeech, int drawCount) : base(id, name, partSpeech)
        {
            DrawCount = drawCount;
        }

        public int DrawCount { get; set; }
    }
}