using System;

namespace Shared.Entities
{
    public class UserModel: BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Token { get; set; }
    }
}
