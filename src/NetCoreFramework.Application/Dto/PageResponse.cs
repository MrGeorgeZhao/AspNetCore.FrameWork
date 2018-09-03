using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto
{
    public class PageResponse : AbstractResponse
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "totalpage")]
        public decimal TotalPage { get; set; }

        [JsonProperty(PropertyName = "pagesize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "pageindex")]
        public int PageIndex { get; set; }
    }
}
