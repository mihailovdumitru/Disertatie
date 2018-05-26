using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateTest.Model
{

    public struct Answer
    {
        public string Content { get; set; }
        public int Points { get; set; }
    }

    public struct Question
    {
        public string Content { get; set; }
        public List<Answer> Answers { get; set; }
    }
    public class Test
    {
        public List<Question> Questions { get; set; }
        public string Lecture { get; set; }
    }
}
