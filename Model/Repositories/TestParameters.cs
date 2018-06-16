﻿using System;

namespace Model.Repositories
{
    public class TestParameters
    {
        public int TestID { get; set; }
        public int TeacherID { get; set; }
        public int ClassID { get; set; }
        public int Duration { get; set; }
        public float Penalty { get; set; }
        public DateTime StartTest { get; set; }
        public DateTime FinishTest { get; set; }
    }
}