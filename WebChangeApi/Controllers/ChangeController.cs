using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebChangeApi.Models;

namespace WebChangeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChangeController : Controller
    {
        [HttpGet("[action]/{value}")]
        public async Task<IActionResult> Change(string Currency1, string Currency2, float value)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    Currency1 = Currency1.ToUpper();
                    Currency2 = Currency2.ToUpper();
                    client.BaseAddress = new Uri("https://api.getgeoapi.com");
                    var response = await client.GetAsync($"/v2/currency/convert?api_key=3646fc16fa24ebde1ec2cbe254f2a75b0a7a9dc3&from={Currency1}&to={Currency2}&amount={value}&format=json");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawChange = JsonConvert.DeserializeObject<ChangeResponse>(stringResult);
                    
                    return Ok(new
                    {
                        Base_currency_code = rawChange.Base_currency_code,
                        Amount = rawChange.Amount,
                        Cuurency_name = rawChange.Rates.UAH.Currency_name,
                        Rate = rawChange.Rates.UAH.Rate,
                        Rate_for_amount = rawChange.Rates.UAH.Rate_for_amountC
                    });
                }
                catch(HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting changer from Currency API: {httpRequestException.Message}");
                }
            }
        }

    }
}
