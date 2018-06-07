using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories
{
    public class Lecture
    {
        public int LectureID { get; set; }
        public string Name { get; set; }
        public int YearOfStudy { get; set; }
        public bool IsActive { get; set; }
    }
}
