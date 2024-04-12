using Diplom.Models.Account;

namespace Diplom.Models.Entity
{
    public class Professor
    {
        public int Id { get; set; }
        public string Lesson { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


        public List<Consultation> Consultations { get; set; }
    }
}
