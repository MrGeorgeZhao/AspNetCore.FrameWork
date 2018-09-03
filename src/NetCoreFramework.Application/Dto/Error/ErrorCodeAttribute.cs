using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.Error
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ErrorCodeAttribute : Attribute
    {
        public string Message { get; set; }
    }
}
