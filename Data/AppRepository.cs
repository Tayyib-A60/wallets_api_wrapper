using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using wallets_api_wrapper.Extensions;
using wallets_api_wrapper.Models;

namespace wallets_api_wrapper.Data
{
    public class AppRepository : IAppRepository
    {
        private IConfiguration _configuration { get; }

        public AppRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public CardDetails GetCardDetails(string cardBin)
        {
            var appParams = new DevParams();
            _configuration.GetSection(nameof(DevParams)).Bind(appParams);

            var url = $"{appParams.BinListApi}{cardBin}";

            var restClient = new RestClient(url);
            var restRequest = new RestRequest("", Method.GET);
            restRequest.AddHeader("Content-Type", "application/json");

            try
            {
                var response = restClient.ExecuteAsync<Dictionary<string, string>>(restRequest).Result;
                
                if(response.IsSuccessful)
                {
                    var cardDetails = BuildResponse(response.Data);
                    return cardDetails;
                }
                else 
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                // Log Exception
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private CardDetails BuildResponse(Dictionary<string, string> data)
        {
            string countryName = String.Empty;
            string countryCurrency = String.Empty;
            string bankName = String.Empty;
            string bankUrl = String.Empty;
            string bankPhone = String.Empty;
            string bankCity = String.Empty;

            data.TryGetValue("scheme", out var schemeData);
            data.TryGetValue("type", out var typeData);
            data.TryGetValue("brand", out var brandData);
            data.TryGetValue("prepaid", out var prepaidInitialData);

            Boolean.TryParse(prepaidInitialData, out bool prepaidData);

            data.TryGetValue("country", out var countryKey);

            if(!String.IsNullOrEmpty(countryKey))
            {
                var countryData = ResponseDataToDictionary(countryKey);
                countryData.TryGetValue("name", out countryName);
                countryData.TryGetValue("currency", out countryCurrency);
            }


            data.TryGetValue("bank", out var bankKey);

            if(!String.IsNullOrEmpty(bankKey))
            {
                var bankData = ResponseDataToDictionary(bankKey);

                bankData.TryGetValue("name", out bankName);
                bankData.TryGetValue("url", out bankUrl);
                bankData.TryGetValue("phone", out bankPhone);
                bankData.TryGetValue("city", out bankCity);
            }


            return new CardDetails
            {
                Scheme = schemeData,
                Type = typeData,
                Brand = brandData,
                Prepaid = prepaidData,
                Country = new Country {
                    Name = countryName,
                    Currency = countryCurrency
                },
                Bank = new Bank {
                    Name = bankName,
                    Url = bankUrl,
                    City = bankCity,
                    Phone = bankPhone
                }
            };
        }
        private Dictionary<string, string> ResponseDataToDictionary(string data)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            return dictionary;
        }
    }
}