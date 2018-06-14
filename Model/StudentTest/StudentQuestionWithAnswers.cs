using System;
using System.Collections.Generic;
using System.Text;

namespace Model.StudentTest
{
    public class StudentQuestionWithAnswers
    {
        public StudentQuestion Question { get; set; }
        public List<StudentAnswer> Answers { get; set; }
    }
}
