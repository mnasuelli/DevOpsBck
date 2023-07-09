using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsBck.Models
{
    public class FilesRepo
    {

        public class RepoFilesRoot
        {
            public int count { get; set; }
            [AllowNull]
            public Value[] value { get; set; }
        }

        public class Value
        {
            [AllowNull]
            public string objectId { get; set; }
            [AllowNull]
            public string gitObjectType { get; set; }
            [AllowNull]
            public string commitId { get; set; }
            [AllowNull]
            public string path { get; set; }
            [AllowNull]
            public bool isFolder { get; set; }
            [AllowNull]
            public string url { get; set; }
        }

    }
}
