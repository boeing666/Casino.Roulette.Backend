using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models.Entity;
using Casino.Roulette.Backend.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Casino.Roulette.Backend.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private HttpClient _httpClient;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public User GetUserInfoByToken(string token)
        {
            var str = $"{_config["SiteApiConnection"]}";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(str)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response =  _httpClient.GetStringAsync($"/getUser/?token={token}").Result;

            var user = JsonConvert.DeserializeObject<User>(response);

            return user;
        }
    }
}
