namespace SimplePainterServer.Dto
{
    public class WordInfoDto
    {
        public WordInfoDto(int id, string name, string partSpeech, int? userId)
        {
            ID         = id;
            Name       = name;
            PartSpeech = partSpeech;
            UserId     = userId;
        }

        public int    ID         { get; set; }
        public string Name       { get; set; }
        public string PartSpeech { get; set; }
        public int?   UserId     { get; set; }
    }
}