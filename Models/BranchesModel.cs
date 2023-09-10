using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsBck.Models
{
    public class BranchesModel
    {

        public class BranchesRootobject
        {
            [AllowNull]
            public Value[] value { get; set; }
            public int count { get; set; }
        }

        public class Value
        {
            [AllowNull]
            public string name { get; set; }
            [AllowNull]
            public string objectId { get; set; }
            [AllowNull]
            public Creator creator { get; set; }
            [AllowNull]
            public string url { get; set; }
        }

        public class Creator
        {
            [AllowNull]
            public string displayName { get; set; }
            [AllowNull]
            public string url { get; set; }
            [AllowNull]
            public _Links _links { get; set; }
            [AllowNull]
            public string id { get; set; }
            [AllowNull]
            public string uniqueName { get; set; }
            [AllowNull]
            public string imageUrl { get; set; }
            [AllowNull]
            public string descriptor { get; set; }
        }

        public class _Links
        {
            [AllowNull]
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            [AllowNull]
            public string href { get; set; }
        }
    }
}
