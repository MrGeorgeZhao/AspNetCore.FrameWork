using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto
{
    public class PageRequest
    {
        [JsonProperty(PropertyName = "pagesize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "pageindex")]
        public int PageIndex { get; set; }
    }
}
