using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto.Error
{
    public static class ErrorCodeHelper
    {
        static Dictionary<ErrorCode, string> cache = new Dictionary<ErrorCode, string>();

        public static string ErrorMessage(this ErrorCode error)
        {
            if (!cache.ContainsKey(error))
            {
                var type = typeof(ErrorCode);
                string name = Enum.GetName(type, error);
                var info = error.GetType().GetField(name);
                var classAttribute = (ErrorCodeAttribute)Attribute.GetCustomAttribute(info, typeof(ErrorCodeAttribute));
                cache[error] = classAttribute.Message;
            }
            return cache[error];
        }
    }
}
