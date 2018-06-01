using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories
{
    public class TeacherDto
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<int> Lectures { get; set; }
    }
}
