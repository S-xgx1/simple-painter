using SimplePainterServer.Dto;

public class GuessDetail : GuessDto
{
    public GuessDetail(int id, int imageId, string word, int userId) : base(id, imageId, word, userId)
    {
    }

    public string SuccessWord { get; set; }
}