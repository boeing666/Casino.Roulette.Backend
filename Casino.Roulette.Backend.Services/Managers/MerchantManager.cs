﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using Casino.Roulette.Backend.Contracts.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Casino.Roulette.Backend.Services.Managers
{
    public class MerchantManager
    {
        private readonly IConfiguration _config;
        public MerchantManager(IConfiguration configuration)
        {
            _config = configuration;
        }



        public void PostBet(MerchantBetModel betModel)
        {
            var str = $"{_config["SiteApiConnection"]}";

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(str)
            };
            var requestModel = JsonConvert.SerializeObject(betModel);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = httpClient.PostAsync($"/api/merchant", new StringContent(requestModel)).Result;

        }
    }
}
