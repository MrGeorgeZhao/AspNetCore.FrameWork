using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.User
{
    public class LoginRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string PassWord { get; set; }

        [JsonProperty(PropertyName = "usertype")]
        public int UserType { get; set; }
    }
}
