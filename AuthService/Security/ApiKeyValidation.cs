namespace AuthService.Security
{
   public class ApiKeyValidation : IApiKeyValidation
    {
        private static bool IsTurnOff = false;
        private readonly IConfiguration _configuration;
        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool IsValidApiKey(string userApiKey)
        {   
            if (IsTurnOff) return true;
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            string? apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
            if (apiKey == null || apiKey != userApiKey)
                return false;
            return true;
        }

        public static void TurnOff() => IsTurnOff = true;
    } 
}

