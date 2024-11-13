namespace SimplePainterServer.Dto
{
    public class ImageInfoDto
    {
        public ImageInfoDto(int id, int wordId, int userId)
        {
            ID     = id;
            WordId = wordId;
            UserID = userId;
        }

        public int ID     { get; set; }
        public int WordId { get; set; }
        public int UserID { get; set; }
    }
}