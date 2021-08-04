using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Responses.Login
{
    public class LoginResponse
    {
        public string CLIENT_TOKEN { get; set; }
        public USER USER { get; set; }
    }

    public class STUDENT
    {
        public string STUDENT_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    public class USER
    {
        public string USERNAME { get; set; }
        public string PROFILE { get; set; }
        public string USER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public object TITLE { get; set; }
        public List<STUDENT> STUDENTS { get; set; }
    }
    
    public class ClientToken
    {
        public bool IsValid { get; set; }
    }

    public class HttpErrorResponse
    {
        public string ERROR { get; set; }
    }

}
