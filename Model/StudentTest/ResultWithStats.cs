using System;
using System.Collections.Generic;
using System.Text;

namespace Model.StudentTest
{
    public class ResultWithStats
    {
        public int NumberOfWrongAnswers { get; set; }
        public int NumberOfCorrectAnswers { get; set; }
        public int NumberOfUnfilledAnswers { get; set; }
        public float Points { get; set; }
        public float Mark { get; set; }
    }
}
