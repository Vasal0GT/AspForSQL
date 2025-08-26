namespace AspForSQL.Models
{
    public class RefreshRequestTokenDTO
    {
        public Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
