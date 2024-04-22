
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApplicationCore.Entities;
using JwtAuthenticationManager.Model;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationManager;

public  class JwtTokenHandler
{
    public const string JWT_SECURITY_KEY = "aqwsedrftgyhujikolpmhbnvccxesssssaaaqwwwhhg";
    private const int JWT_TOKEN_VALIDITY_MINS = 20;
    private readonly List<UserAccount> userAccounts;
    public JwtTokenHandler()
    {
        userAccounts = new()
        {
            new UserAccount() { Email = "admin", Password = "admin@123", Role = "Admin" },
            new UserAccount() { Email = "user", Password = "user@123", Role = "User" }
        };
    }
    
    
    public AuthenticationResponse GenerateToken(User user)
    {
        //should be verifies
        if (user == null) return null;
    
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        var tokenExpireTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var claimsIdentity = new ClaimsIdentity(new List<Claim> {
            //new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim("Role",user.Role)
    
        });
    
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature
        );
    
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpireTimeStamp,
            SigningCredentials = signingCredentials,
            NotBefore = DateTime.Now,
    
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
    
        return new AuthenticationResponse {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            JwtToken = token,
            ExpiresIn =(int) tokenExpireTimeStamp.Subtract(DateTime.Now).TotalSeconds
        };
    }
    
    
    
    public AuthenticationResponse GenerateTokenFromMockUser(AuthenticationRequest authenticationRequest)
    {
        if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
            return null;
        //validate the user
        var user = userAccounts.Where(x => x.Email == authenticationRequest.Email && x.Password == authenticationRequest.Password).FirstOrDefault();
        if (user == null)
            return null;

        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        var tokenExpireTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var claimsIdentity = new ClaimsIdentity(new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim("Role",user.Role)

        });
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature
        );
        //Console.WriteLine("in JWT Handler 1 ********");
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpireTimeStamp,
            SigningCredentials = signingCredentials,
            NotBefore = DateTime.Now,
        };
        Console.WriteLine("in JWT Handler 1 ********");
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        Console.WriteLine("in JWT Handler 3 ********");

        return new AuthenticationResponse {
            Email = user.Email,
            JwtToken = token,
            ExpiresIn =(int) tokenExpireTimeStamp.Subtract(DateTime.Now).TotalSeconds
        };
    }
    

}