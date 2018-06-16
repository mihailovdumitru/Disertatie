namespace Services.Endpoints
{
    public class RepositoriesEndpoint
    {
        public static string AddLecture = "lectures";
        public static string GetLectures = "lectures";
        public static string DeleteLecture = "lectures/{0}";
        public static string GetTeachersLecture = "beginTest?teacherID={0}";
        public static string UpdateLecture = "lectures/{0}";
        public static string GetTeachers = "teachers";
        public static string GetTeacher = "teachers/{0}";
        public static string DeleteTeacher = "teachers/{0}";
        public static string UpdateTeacher = "teachers/{0}";
        public static string AddTeacher = "teachers";
        public static string AddStudent = "students";
        public static string UpdateStudent = "students/{0}";
        public static string DeleteStudent = "students/{0}";
        public static string UpdateUser = "users/{0}";
        public static string AddUser = "users";
        public static string GetStudents = "students";
        public static string AddClass = "classes";
        public static string DeleteClass = "classes/{0}";
        public static string GetClasses = "classes";
        public static string UpdateClass = "classes/{0}";
        public static string GetTests = "tests";
        public static string GetTeacherAuthObj = "auth/teacherAuth?email={0}";
        public static string AddTestParams = "tests";
        public static string GetTestsParams = "StudentTest/GetTestsParameters";
        public static string GetUserByUsername = "users/username/{0}";
        public static string GetTest = "StudentTest/GetTest/{0}";
        public static string AddTestResults = "StudentTest/TestResults";
        public static string GetTestResults = "StudentTest/TestResults";
        public static string UpdateTest = "test";
    }
}