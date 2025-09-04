using AspForSQL;
using HttpClientForSQL.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientForSQL
{
    public class LoginResult
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public bool IsSuccess { get; set; }
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class TokenResponse
    {
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class APIServer
    {
        
        //поля классов для дальнейшей работы
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7089/api/";

        //а это у нас конструктор класса
        public APIServer()
        {
            _httpClient = new HttpClient();

            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<List<Library>> GetLibrariesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}libraries");
            response.EnsureSuccessStatusCode();//проверяет успешность запроса, если че не так килает ошибку
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Library>>(content);//тут типо контент из json в указаный файл
        }

        public async Task<Library> GetLibraryAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}libraries/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Library>(content);
        }

        public async Task<int> UpdateLibraryAsync(int id, Library library)
        {
            var response = await _httpClient.PutAsJsonAsync<Library>($"{_baseUrl}libraries/{id}", library);
            response.EnsureSuccessStatusCode();
            return (int)response.StatusCode;
        }

        public async Task<int> DeleteLibraryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}libraries/{id}");
            response.EnsureSuccessStatusCode();
            return ((int)response.StatusCode);
        }

        public async Task<int> PostLibraryAsync(Library library)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}libraries", library);
            response.EnsureSuccessStatusCode();
            return ((int)response.StatusCode);
        }

        public async Task<LoginResult> LoginAsync(UserDTOclient user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}Auth/login", user);
            LoginResult result = new LoginResult
            {
                StatusCode = (int)response.StatusCode,
                StatusDescription = response.StatusCode.ToString(),
                IsSuccess = response.IsSuccessStatusCode
            };
            if (result.IsSuccess == true)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);
                result.AccesToken = tokenResponse.AccesToken;
                result.RefreshToken = tokenResponse.RefreshToken;

                editHeader(result.AccesToken);
                return result;
            }
            else
            {  
                return null; 
            }
        }

        public void editHeader(string result)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);
        }
    }
}
