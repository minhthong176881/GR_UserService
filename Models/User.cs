using System;

namespace UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressOfBirth { get; set; }
        public string Nationality { get; set; }
        public string IdentityNumber { get; set; }
        public int IsActive { get; set; }
        // public bool IsDeleted { get; set; }
        // public bool IsEmailConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}