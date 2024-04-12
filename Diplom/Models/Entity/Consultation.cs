using Diplom.Models.Account;

namespace Diplom.Models.Entity
{
    public class Consultation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public List<Subscription> Subscriptions { get; set; }
 
    }
}
