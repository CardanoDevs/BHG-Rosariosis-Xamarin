using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Responses.Login
{
   public class SchoolResponse
    {
        public List<School> Schools { get; set; }
    }

    public class School
    {
        public string Name { get; set; }
        public string BaseURL { get; set; }
    }

}
