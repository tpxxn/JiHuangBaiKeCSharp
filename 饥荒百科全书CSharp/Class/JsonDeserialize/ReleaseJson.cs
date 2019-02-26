using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{
    public class UpdateContent
    {
        public string Label { get; set; }
        public string Content { get; set; }
    }

    public class Release
    {
        public string Version { get; set; }
        public string Data { get; set; }
        public List<UpdateContent> UpdateContent { get; set; }

        public Release()
        {
            UpdateContent = new List<UpdateContent>();
        }
    }

    public class ReleaseRootObject
    {
        public List<Release> Release { get; set; }

        public ReleaseRootObject()
        {
            Release = new List<Release>();
        }
    }
}
