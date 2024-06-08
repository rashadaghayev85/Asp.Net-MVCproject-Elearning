namespace MVCproject_Elearning.Models
{
    public class Student:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string Profession { get; set; }
    }
}
