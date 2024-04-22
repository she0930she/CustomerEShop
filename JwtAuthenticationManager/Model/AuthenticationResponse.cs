namespace JwtAuthenticationManager.Model;

public class AuthenticationResponse
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string JwtToken { get; set; }
    public int ExpiresIn { get; set; }
}