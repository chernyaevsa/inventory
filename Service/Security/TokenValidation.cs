using Org.BouncyCastle.Asn1.Cms;

namespace Service.Security
{
   public class TokenValidation : ITokenValidation
    {
        private static bool IsTurnOff = false;
        private readonly IConfiguration _configuration;
        public TokenValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> IsValidToken(string _token)
        {   
            if (IsTurnOff) return true;
            var authService = new AuthService(_configuration);
            if (await authService.GetUserName(_token) == null){
                return false;
            }
            return true;
        }        
        
        public static void TurnOff() => IsTurnOff = true;
    } 
}

