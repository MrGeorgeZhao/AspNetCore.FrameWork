using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.User
{
    public class LoginResponse : AbstractResponse
    {
        [JsonProperty(PropertyName = "issuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public long Uid { get; set; }

        [JsonProperty(PropertyName = "roles")]
        public List<int> Role { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int UserType { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "menus")]
        public List<MenuDto> Menus { get; set; }

    }

    public class MenuDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "subs")]
        public List<MenuDto> Subs { get; set; }
    }
}
