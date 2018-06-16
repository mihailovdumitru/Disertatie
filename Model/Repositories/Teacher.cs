using System.Collections.Generic;

namespace Model.Repositories
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<int> Lectures { get; set; }
        public int UserID { get; set; }
    }
}