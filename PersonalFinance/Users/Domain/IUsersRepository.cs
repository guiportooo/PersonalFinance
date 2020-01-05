using System;
using System.Threading.Tasks;
using PersonalFinance.Common;

namespace PersonalFinance.Users.Domain
{
    public interface IUsersRepository
    {
        Task<Result<User>> Get(Guid id);
        Task<Result> Add(User user);
    }
}