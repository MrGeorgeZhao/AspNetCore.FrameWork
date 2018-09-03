using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Interfaces
{
    public interface IEncryptionService
    {
        string GetDigestedPassword(string plain, string salt);

        string GenerateSalt();
    }
}
