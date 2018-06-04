using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Endpoints
{
    public class RepositoriesEndpoint
    {
        public static string AddLecture = "lectures";
        public static string GetLectures = "lectures";
        public static string UpdateLecture = "lectures/{0}";
        public static string GetTeachers = "teachers";
        public static string UpdateTeacher = "teachers/{0}";
        public static string AddTeacher = "teachers";
        public static string AddStudent = "students";
        public static string UpdateStudent = "students/{0}";
        public static string GetStudents = "students";
        public static string AddClass = "classes";
        public static string GetClasses = "classes";
        public static string UpdateClass = "classes/{0}";
    }
}
