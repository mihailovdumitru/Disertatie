﻿namespace Model.Repositories
{
    public class QuestionWithAnswer
    {
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public bool Correct { get; set; }
    }
}