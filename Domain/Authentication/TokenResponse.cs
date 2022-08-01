namespace Domain.Authentication
{
    public class TokenResponse
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
