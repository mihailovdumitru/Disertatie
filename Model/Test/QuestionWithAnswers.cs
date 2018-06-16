using System.Collections.Generic;

namespace Model.Test
{
    public class QuestionWithAnswers
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}