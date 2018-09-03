using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto
{
    public class NameValueResponse : AbstractResponse
    {
        [JsonProperty(PropertyName = "resuls")]
        public List<NameValueDto> Results { get; set; }
    }

    public class NameValueDto
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
