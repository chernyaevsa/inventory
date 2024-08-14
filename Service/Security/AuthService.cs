using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Service.Security
{
   public class AuthService
    {
        private string? _address = "http://127.0.0.1";
        private string? _apiKey = "";
        public AuthService(IConfiguration configuration) {
            _address = configuration.GetValue<string>(Constants.AuthServiceAddressName);
            _apiKey = configuration.GetValue<string>(Constants.ApiKeyName);
        }

        private HttpClient? GetConnection(){
            if (_address == null) {
                Logger.Print(Logger.ERROR_MESSAGE, "GetConnection()", "Can't find address of auth service. Check your enviroment variables.");
                return null;
            }
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http.BaseAddress = new Uri(_address);
            return http;
        }

        public async Task<string?> GetUserName(string _token){
            if (string.IsNullOrWhiteSpace(_token)) {
                Logger.Print(Logger.ERROR_MESSAGE, "GetUserName(token)", "Token is null or white space!");
                return null;
            }
            
            var http = GetConnection();
            if (http == null) return null;
            
            var requestBody = new {
                apiKey = _apiKey,
                token = _token
            };
            var jsonData = JsonSerializer.Serialize(requestBody);
            
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await http.PostAsync("/token/check", stringContent);
            
            if (!result.IsSuccessStatusCode) {
                var errorString = await result.Content.ReadAsStringAsync();
                Logger.Print(Logger.ERROR_MESSAGE, "GetUserName(token)", $"Status code is not succeed. Message {errorString}");
                return null;
            }

            var resultBody = await result.Content.ReadFromJsonAsync<JsonNode>();
            if (resultBody == null) {
                Logger.Print(Logger.ERROR_MESSAGE, "GetUserName(token)", $"Response body from server was null!");
                return null;
            }
            var content = resultBody["content"];
            if (content == null){
                Logger.Print(Logger.ERROR_MESSAGE, "GetUserName(token)", $"Content field from server was null!");
                return null;
            }
            var login = content["login"];
            if (login == null){
                Logger.Print(Logger.ERROR_MESSAGE, "GetUserName(token)", $"Login field from server was null!");
                return null;
            }

            return login.ToString();

            
            
        }
    } 
}

