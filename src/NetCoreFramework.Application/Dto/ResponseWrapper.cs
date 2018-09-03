using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using NetCoreFramework.Application.Dto.Error;

namespace NetCoreFramework.Application.Dto
{
    public class ResponseWrapper<T> where T : AbstractResponse
    {
        [JsonProperty(PropertyName = "haserror")]
        public bool HasError => Error != null;

        [JsonProperty(PropertyName = "error")]
        public ErrorResponse Error { get; set; }

        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
}
