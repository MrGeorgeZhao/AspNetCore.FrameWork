using System;
using System.Collections.Generic;
using System.Text;
using NetCoreFramework.Application.Interfaces;
using NetCoreFramework.Infra.Util.Helper;

namespace NetCoreFramework.Application.Services
{
    public class EncryptionService : BaseService, IEncryptionService
    {
        public string GetDigestedPassword(string plain, string salt)
        {
            return Digest.SHA256Of(plain + salt);
        }

        public string GenerateSalt()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
