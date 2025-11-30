using DataAccess.Abstract;
using Entitites;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete
{
    public class MemberAPIDal : IMemberAPIDal
    {
        private readonly HttpClient _httpClient;
        private readonly string _mainApiUrl;

        public MemberAPIDal(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // GetValue yerine GetSection ve Value kullanılmalı
            _mainApiUrl = configuration.GetSection("ApiSettings:MainApiUrl").Value;
        }

        public async Task<List<MemberDto>> GetAllMembers()
        {
            // Hard-coded adres yerine ayarlar dosyasından gelen adresi kullan
            var response = await _httpClient.GetAsync("https://localhost:44368/api/Member/getbydate");

            if (response.IsSuccessStatusCode)
            {
                var members = await response.Content.ReadFromJsonAsync<List<MemberDto>>();
                return members;
            }

            return new List<MemberDto>();
        }
    }
}
