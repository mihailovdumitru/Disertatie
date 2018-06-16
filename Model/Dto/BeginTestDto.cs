using Model.Repositories;
using System.Collections.Generic;

namespace Model.Dto
{
    public class BeginTestDto
    {
        public List<Lecture> lectures;
        public List<Test.Test> tests;
        public List<StudyClass> classes;
    }
}