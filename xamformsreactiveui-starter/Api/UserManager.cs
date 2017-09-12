using System;
using System.Threading.Tasks;
using xamformsreactiveuistarter.Models;

namespace xamformsreactiveuistarter.Api
{
    public class UserManager
    {
        IRestService restService;

        public UserManager(IRestService service)
        {
            restService = service;
        }

        public Task<LoginResponse> Login(string username, string password)
        {
            return restService.Login(username, password);
        }
    }
}
