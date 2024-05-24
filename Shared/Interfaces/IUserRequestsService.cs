using Shared.DTO.ServiceBusDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IUserRequestsService
    {
        Task<UserRights> GetUserRights(string email);
        Task<UserRights> GetUserRights(Guid Id);
        Task AddDocumentToUser(AddAttributeToUserRequest req);
        Task HandleDocumentRequest(Guid applicantId, Guid documentGuid);
        Task HandleDocumentRequest(string applicantEmail, Guid documentGuid);
    }
}
