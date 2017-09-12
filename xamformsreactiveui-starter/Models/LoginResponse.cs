using System;
namespace xamformsreactiveuistarter.Models
{
    public class LoginResponse
    {
        public string access_token { get; set; }
        public string error_description { get; set; }
        public string error { get; set; }
    }
}
