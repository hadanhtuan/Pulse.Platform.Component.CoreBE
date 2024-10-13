using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Shared.Core.Interfaces.Permissions
{
    public interface IUserInfo
    {
        Guid UserId { get; }

        Guid ActiveRole { get; }
    }
}
