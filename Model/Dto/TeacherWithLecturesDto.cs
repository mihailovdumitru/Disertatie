using Model.Repositories;
using System.Collections.Generic;

namespace Model.Dto
{
    public class TeacherWithLecturesDto
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Lecture> Lectures { get; set; }
    }
}