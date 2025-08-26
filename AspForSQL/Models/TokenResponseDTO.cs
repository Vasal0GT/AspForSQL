namespace AspForSQL.Models
{
    public class TokenResponseDTO
    {
        public required string AccesToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
