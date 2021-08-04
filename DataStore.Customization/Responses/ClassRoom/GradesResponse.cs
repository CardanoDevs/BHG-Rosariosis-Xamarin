using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Responses.ClassRoom
{
   public  class GradesResponse
    {
        public List<DETAIL> Grades { get; set; }
    }

    public class DETAIL
    {
        public string ASSIGNMENT_ID { get; set; }
        public string TITLE { get; set; }
        public string CATEGORY { get; set; }
        public string POINTS_EARNED { get; set; }
        public string POINTS_TOTAL { get; set; }
        public string PERCENT { get; set; }
        public string GRADE { get; set; }
        public string GPA { get; set; }
        public string COMMENT { get; set; }
    }

    //public class Grade
    //{
    //    public int COURSE_PERIOD_ID { get; set; }
    //    public string COURSE_TITLE { get; set; }
    //    public int TEACHER_ID { get; set; }
    //    public string TEACHER_NAME { get; set; }
    //    public int UNGRADED { get; set; }
    //    public int PERCENT { get; set; }
    //    public string LETTER { get; set; }
    //    public List<DETAIL> DETAILS { get; set; }
    //}
}
