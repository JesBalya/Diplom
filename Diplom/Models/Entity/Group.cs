namespace Diplom.Models.Entity
{
    public class Group
    {
        public int Id { get; set; }

        public int Number { get; set; }


/*                 Relations                  */
    public List<Student> Students { get; set; }
    public List<Consultation> Consultations {  get; set; }
        
    
    }
}
