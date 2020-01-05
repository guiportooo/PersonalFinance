using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinance.Common;
using PersonalFinance.Users.Domain;

namespace PersonalFinance.Users
{
    public class UsersRepository : IUsersRepository
    {
        private static List<User> users = new List<User>();

        public UsersRepository()
        {
            users.Add(new User(new Guid("32e3e24f-5c52-4909-9fc5-c2e5b6b8e4b8"), "Fake", "User"));
            users.Add(new User(new Guid("252a0090-013d-4b6d-929c-16849b72079c"), "Another", "FakeUser"));
            users.Add(new User(new Guid("4b848fb8-e1aa-4858-995c-c3c5676737ee"), "AnotherFake", "User3"));
        }

        public Task<Result<User>> Get(Guid id) =>
            users.FirstOrDefault(u => u.Id == id) switch
            {
                { } user => Task.FromResult(Result.Ok(user)),
                _ => Task.FromResult(Result.Fail<User>($"Could not found user with id {id}."))
            };

        public async Task<Result> Add(User user) =>
            await Get(user.Id) switch
            {
                { IsSuccess: true } => Result.Fail($"There is already an user with the id {user.Id}."),
                { IsSuccess: false } => AddUser(user),
                _ => Result.Fail("Fail to add user.")
            };

        private static Result AddUser(User user)
        {
            users.Add(user);
            return Result.Ok();
        }
    }
}