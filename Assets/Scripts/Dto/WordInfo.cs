namespace SimplePainterServer.Dto
{
    public class WordInfoDto
    {
        public WordInfoDto(int id, string name, string partSpeech)
        {
            ID         = id;
            Name       = name;
            PartSpeech = partSpeech;
        }

        public int    ID         { get; set; }
        public string Name       { get; set; }
        public string PartSpeech { get; set; }
    }
}