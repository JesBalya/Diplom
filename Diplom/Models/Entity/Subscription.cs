using Diplom.Models.Account;

namespace Diplom.Models.Entity
{
    public class Subscription
    {
        public int Id {  get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public List<Consultation>? Consultations { get; set; } = new List<Consultation>();
    }
}
