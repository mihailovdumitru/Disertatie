using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
