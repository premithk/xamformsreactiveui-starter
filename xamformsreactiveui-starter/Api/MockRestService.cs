using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using xamformsreactiveuistarter.Models;

namespace xamformsreactiveuistarter.Api
{
    public class MockRestService:IRestService
    {
        public async Task<LoginResponse> Login(string username, string password)
        {
            var response = new LoginResponse();

            await Task.Delay(500);

            if (username.ToLower() == "test" && password == "test")
            {
                response.access_token = "uoiuiuiuiu";
                return response;
            }
            else
            {
                response.error_description = "Invalid username or password";
                return response;                
            }
                
        }
    }
}
