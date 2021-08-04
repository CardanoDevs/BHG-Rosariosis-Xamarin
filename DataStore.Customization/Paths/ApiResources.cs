using Plugin.SecureStorage;

namespace DataStore.Customization.Paths
{
    // https://gorest.co.in/
    // API details about Features, Authentication, Response Codes

    public static class ApiResources
    {
        public static readonly string Login = $"/api/Login.php";
        public static readonly string VerifyToken = $"/api/VerifyJWT.php";
        public static readonly string CoursePeriod = $"/api/CoursePeriods.php";
        public static readonly string Course = $"/api/Course.php";
        public static readonly string Annoucements = $"/api/Announcements.php";
        public static readonly string Assignment = $"/api/Assignments.php";
        public static readonly string Grades = $"/api/Grades.php";
        public static readonly string FCMToken = $"/api/UpdateFCMToken.php";
        public static readonly string Signup = $"Signup/";
        public static readonly string Quiz = $"/api/Quiz.php";
    }
}
