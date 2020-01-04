using System;
using System.Threading.Tasks;
using PersonalFinance.Common;
using PersonalFinance.Users.Models;

namespace PersonalFinance.Users
{
    public interface IUsersService
    {
        Task<Result<UserResponse>> GetUser(Guid id);
    }
}