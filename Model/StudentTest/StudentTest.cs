using System.Collections.Generic;

namespace Model.StudentTest
{
    public class StudentTest
    {
        public int TestID { get; set; }
        public int LectureID { get; set; }
        public List<StudentQuestionWithAnswers> Questions { get; set; }
        public string Naming { get; set; }
        public int TeacherID { get; set; }
    }
}