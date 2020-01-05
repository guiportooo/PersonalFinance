using System;

namespace PersonalFinance.Users.Models
{
    public class UserCreatedResponse
    {
        public Guid Id { get; }

        public UserCreatedResponse(Guid id) => Id = id;
    }
}