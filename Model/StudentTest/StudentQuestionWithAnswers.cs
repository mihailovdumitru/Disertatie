using System.Collections.Generic;

namespace Model.StudentTest
{
    public class StudentQuestionWithAnswers
    {
        public StudentQuestion Question { get; set; }
        public List<StudentAnswer> Answers { get; set; }
    }
}