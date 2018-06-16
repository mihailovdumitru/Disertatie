using System.Collections.Generic;

namespace Model.StudentTest
{
    public class ResultWithAnswers
    {
        public int QuestionID { get; set; }
        public List<int> Answers { get; set; }
    }
}