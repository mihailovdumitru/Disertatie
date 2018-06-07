using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dto
{
    public class BeginTestDto
    {
        public List<Lecture> lectures;
        public List<Test.Test> tests;
        public List<StudyClass> classes;

    }
}
