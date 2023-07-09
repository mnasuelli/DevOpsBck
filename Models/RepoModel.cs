using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsBck.Models
{
    public class RepoModel
    {

        public class RepoRoot
        {
            public int count { get; set; }
            [AllowNull]
            public Value[] value { get; set; }
        }
        public class Value
        {
            [AllowNull]
            public string id { get; set; }
            [AllowNull]
            public string name { get; set; }
            [AllowNull]
            public string url { get; set; }
            [AllowNull]
            public Project project { get; set; }
            [AllowNull]
            public string remoteUrl { get; set; }
            [AllowNull]
            public string defaultBranch { get; set; }
        }

        public class Project
        {
            [AllowNull]
            public string id { get; set; }
            [AllowNull]
            public string name { get; set; }
            [AllowNull]
            public string url { get; set; }
            [AllowNull]
            public string state { get; set; }
        }

    }
}
