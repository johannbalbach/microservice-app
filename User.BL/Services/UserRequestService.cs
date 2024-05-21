using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.ServiceBusDTO;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Context;

namespace User.BL.Services
{
    public class UserRequestService: IUserRequestsService
    {
        private readonly AuthDbContext _context;
        public UserRequestService(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<UserRights> GetUserRights(string email)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);

            if (temp == null)
                throw new InvalidLoginException("user with that email doesnt exist");

            return new UserRights { Id = temp.Id, Roles = temp.Roles };
        }
        public async Task<UserRights> GetUserRights(Guid Id)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Id == Id);

            if (temp == null)
                throw new InvalidLoginException("user with that Guid doesnt exist");

            return new UserRights { Id = temp.Id, Roles = temp.Roles };
        }
        public async Task AddDocumentToUser(AddAttributeToUserRequest req)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(u => u.Id == req.UserId);
            if (applicant == null)
                throw new InvalidLoginException("user with that Guid doesnt exist");

            switch (req.UserAttributes) 
            {
                case UserAttributes.Enrollment:
                    applicant.Enrollments.Add(req.AttributeId);
                    await _context.SaveChangesAsync();
                    return;
                case UserAttributes.Document:
                    applicant.Documents.Add(req.AttributeId);
                    await _context.SaveChangesAsync();
                    return;
            }
        }
    }
}
