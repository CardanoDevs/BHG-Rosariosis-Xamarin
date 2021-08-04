using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataStore.Customization.Responses.ClassRoom
{
    public class AssignmentResponse
    {
        public List<Assignment> Assignments { get; set; }
    }

    public class Assignment
    {
        public bool IS_QUIZ { get; set; }
        public string STUDENT_ID { get; set; }
        public string ASSIGNMENT_ID { get; set; }
        public string ASSIGNMENT_TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string ASSIGNED_DATE { get; set; }
        public string DUE_DATE { get; set; }
        public string ASSIGNMENT_TYPE_ID { get; set; }
        public string ASSIGNMENT_TYPE { get; set; }
        public string POINTS { get; set; }
        public string POINTS_EARNED { get; set; }
        public bool IS_CHECKED { get; set; }
        public string GRADE { get; set; }
        public int PERCENTAGE { get; set; }
        public string GPA { get; set; }       
        public Color bgColor { get; set; }
        public Color MyAwesomeFC { get; set; }

    }
}
