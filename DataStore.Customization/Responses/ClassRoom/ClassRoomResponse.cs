using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Responses.ClassRoom
{
    public class ClassRoomResponse
    {
        public List<COURS> COURSES { get; set; }
    }

    public class COURS
    {
        public string STUDENT_ID { get; set; }
        public string COURSE_ID { get; set; }
        public string COURSE_PERIOD_ID { get; set; }
        public string TITLE { get; set; }
        public string CP_TITLE { get; set; }
        public string ROOM { get; set; }
        public string TEACHER_ID { get; set; }
        public string TEACHER_NAME { get; set; }
        public string PERCENTAGE { get; set; }
        public string PERIOD { get; set; }
        public string GRADE { get; set; }
        public string BgCardImage { get; set; }
        public string GPA { get; set; }
    }
}
