using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreFramework.Domain.Models;

namespace NetCoreFramework.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<List<User>> GetUserByName(string name);
    }
}