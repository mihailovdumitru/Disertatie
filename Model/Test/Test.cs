using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Test
{
    public class Test
    {
        public int TestID { get; set; }
        public int LectureID { get; set; }
        public List<QuestionWithAnswers> Questions { get; set; }
        public string Naming { get; set; }
        public int TeacherID { get; set; }
    }
}
