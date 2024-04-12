using Diplom.Models.Account;

namespace Diplom.Models.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
