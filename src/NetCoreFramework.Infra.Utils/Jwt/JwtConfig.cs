using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Infra.Util.Jwt
{
    public class JwtConfig
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresHour { get; set; }
    }
}
