using System;
using System.Collections.Generic;

namespace NetCoreFramework.Domain.Models
{
    public class User
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public long Id { get; set; }

        public string PassWord { get; set; }

        public long MemberId { get; set; }

        public string Salt { get; set; }
    }
}