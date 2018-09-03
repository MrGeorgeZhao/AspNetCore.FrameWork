using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Application.Dto
{
    public interface IRequest<out T> where T : AbstractResponse
    {

    }
}
