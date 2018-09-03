using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto
{
    public class StatusRequest
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "uid")]
        public long Uid { get; set; }
    }
}
