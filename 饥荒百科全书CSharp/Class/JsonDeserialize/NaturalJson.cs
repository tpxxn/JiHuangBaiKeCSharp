using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{
    public class Nature
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public List<string> Abundant { get; set; }
        public List<string> Occasional { get; set; }
        public List<string> Rare { get; set; }
        public string Introduction { get; set; }

        public Nature()
        {
            Abundant = new List<string>();
            Occasional = new List<string>();
            Rare = new List<string>();
        }
    }

    public class Biomes
    {
        public List<Nature> Nature { get; set; }

        public Biomes()
        {
            Nature = new List<Nature>();
        }
    }

    public class NaturalRootObject
    {
        public Biomes Biomes { get; set; }

        public NaturalRootObject()
        {
            Biomes = new Biomes();
        }
    }
}
