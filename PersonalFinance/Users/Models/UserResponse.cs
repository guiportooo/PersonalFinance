using System;
using PersonalFinance.Users.Domain;

namespace PersonalFinance.Users.Models
{
    public class UserResponse
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }

        public UserResponse(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FullName = user.FullName;
        }
    }
}