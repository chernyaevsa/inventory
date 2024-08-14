namespace Service.Security
{
   public interface ITokenValidation
    {
        Task<bool> IsValidToken(string userApiKey);
    } 
}

