using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Customization.Requests.ClassRoom
{
    public class AssignmentDetailRequest
    {
        public List<FileWrapper> Files = new List<FileWrapper>();
        public string Message { get; set; }
    }

    public class FileWrapper
    {
        public byte[] FileData { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }

}
