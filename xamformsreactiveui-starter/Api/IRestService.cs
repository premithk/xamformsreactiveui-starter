using System;
using System.Threading.Tasks;
using xamformsreactiveuistarter.Models;

namespace xamformsreactiveuistarter.Api
{
    public interface IRestService
    {
        Task<LoginResponse> Login(string username, string password);
    }
}
