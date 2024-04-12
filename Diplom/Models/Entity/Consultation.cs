namespace Diplom.Models.Entity
{
    public class Consultation
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }



        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }        
    }
}
