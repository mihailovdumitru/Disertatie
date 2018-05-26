using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateTest.Model
{

    public struct Answer
    {
        public string Content { get; set; }
        public string Correct { get; set; }
    }

    public struct QuestionObj
    {
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
        public int Points { get; set; }
    }

    public class Test
    {
        public string Lecture { get; set; }
        public List<QuestionObj> Questions { get; set; }
    }
}
