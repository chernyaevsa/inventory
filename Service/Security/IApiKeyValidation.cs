namespace Service.Security
{
   public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    } 
}

