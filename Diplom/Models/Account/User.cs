using Diplom.Models.Entity;


namespace Diplom.Models.Account
{
    public class User
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }

        public Subscription Subscription { get; set; }

        public ICollection<Consultation> MyConsultations { get; set; } = new List<Consultation>();
    }
}
