using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Responses.ClassRoom
{
    public class AssignmentDetailResponse
    {
        public AssignmentsDetail Assignment { get; set; }
    }

    public class AssignmentsDetail
    {
        public List<FileResponseWrapper> Files { get; set; }
        public string Message { get; set; }
    }

    public class FileResponseWrapper
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }

}
