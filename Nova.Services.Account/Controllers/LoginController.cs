using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nova.Services.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> GetTokenAsync(LoginParameter parameter)
        {
            var dict = new Dictionary<string, string>
            {
                ["client_id"] = "C_ID_1",
                ["client_secret"] = "C_SECRET_1",
                ["grant_type"] = "password",
                ["username"] = parameter.UserName,
                ["password"] = parameter.PassWord
            };

            using (HttpClient hc = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(dict))
                {
                    string authUrl = Environment.GetEnvironmentVariable("AUTH_URL");
                    var msg = await hc.PostAsync(authUrl, content);

                    if (msg.IsSuccessStatusCode)
                    {
                        string rst = await msg.Content.ReadAsStringAsync();
                        return Content(rst, "application/json");
                    }
                    else
                    {
                        return StatusCode((int)msg.StatusCode);
                    }
                }
            }
        }

        public class LoginParameter
        {
            public string UserName { get; set; }

            public string PassWord { get; set; }
        }
    }
}