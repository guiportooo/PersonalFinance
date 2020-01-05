using System;
using System.Threading.Tasks;
using PersonalFinance.Common;
using PersonalFinance.Users.Domain;
using PersonalFinance.Users.Models;

namespace PersonalFinance.Users
{
    public interface IUsersService
    {
        Task<Result<UserResponse>> GetUser(Guid id);
        Task<Result<UserCreatedResponse>> CreateUser(CreateUserRequest request);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) => _usersRepository = usersRepository;

        public async Task<Result<UserResponse>> GetUser(Guid id) =>
            await _usersRepository.Get(id) switch
            {
                { IsSuccess: true } result => Result.Ok(new UserResponse(result.Value)),
                { IsSuccess: false } result => Result.Fail<UserResponse>(result.Error),
                _ => Result.Fail<UserResponse>($"Fail to get user with id {id}.")
            };

        public async Task<Result<UserCreatedResponse>> CreateUser(CreateUserRequest request)
        {
            var id = Guid.NewGuid();
            return await _usersRepository.Add(new User(id, request.FirstName, request.LastName)) switch
            {
                { IsSuccess: true } => Result.Ok(new UserCreatedResponse(id)),
                { IsSuccess: false } result => Result.Fail<UserCreatedResponse>(result.Error),
                _ => Result.Fail<UserCreatedResponse>("Fail to create user.")
            };
        }
    }
}