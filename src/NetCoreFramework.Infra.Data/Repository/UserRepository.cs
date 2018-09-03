using System.Linq;
using NetCoreFramework.Domain.Interfaces;
using NetCoreFramework.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;

namespace NetCoreFramework.Infra.Data.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connect) : base(connect)
        {

        }

        public async Task<List<User>> GetUserByName(string name)
        {

            var ret = await ExecuteWithConditionAsync(async p => (await p.QueryAsync<User>("select Name,MemberId, UserName, PassWord,Salt,Type UserType,r.RoleID as  UserRole  from [user] u LEFT JOIN UserRole r ON u.id=r.UserID  where UserName=@name and IsDelete=0", new { name = name })));
            return ret?.ToList();
        }
    }
}
