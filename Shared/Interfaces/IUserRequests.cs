using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IUserRequests
    {
        Task<UserRights> GetUserRights(string email);
        Task<UserRights> GetUserRights(Guid Id);
    }
}
