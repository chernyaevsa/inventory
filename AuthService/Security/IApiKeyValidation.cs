namespace AuthService.Security
{
   public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    } 
}

