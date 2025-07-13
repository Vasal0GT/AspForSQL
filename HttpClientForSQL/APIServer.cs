using AspForSQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientForSQL
{
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
            var response = await _httpClient.GetAsync($"{_baseUrl}libraries{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Library>(content);
        }
    }
}
