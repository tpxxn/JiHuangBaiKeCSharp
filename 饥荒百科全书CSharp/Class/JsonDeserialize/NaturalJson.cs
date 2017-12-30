using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{
    public class NatureBiomes
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public List<string> Abundant { get; set; }
        public List<string> Occasional { get; set; }
        public List<string> Rare { get; set; }
        public string Introduction { get; set; }

        public NatureBiomes()
        {
            Abundant = new List<string>();
            Occasional = new List<string>();
            Rare = new List<string>();
        }
    }

    public class Biomes
    {
        public List<NatureBiomes> NatureBiomes { get; set; }
        
        public Biomes()
        {
            NatureBiomes = new List<NatureBiomes>();
        }
    }

    public class NatureSmallPlant
    {
        public string Picture { get; set; }
        public List<string> Pictures { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public List<List<string>> Resources { get; set; }
        public List<string> ResourcesBurned { get; set; }
        public List<string> Ability { get; set; }
        public bool IsRegenerate { get; set; }
        public bool IsCombustible { get; set; }
        public List<string> Biomes { get; set; }
        public string Introduction { get; set; }
        public string Console { get; set; }

        public NatureSmallPlant()
        {
            Pictures = new List<string>();
            Resources = new List<List<string>>();
            ResourcesBurned = new List<string>();
            Ability = new List<string>();
            Biomes = new List<string>();
        }
    }

    public class SmallPlant
    {
        public List<NatureSmallPlant> NatureSmallPlant { get; set; }

        public SmallPlant()
        {
            NatureSmallPlant = new List<NatureSmallPlant>();
        }
    }

    public class NaturalRootObject
    {
        public Biomes Biomes { get; set; }
        public SmallPlant SmallPlant { get; set; }

        public NaturalRootObject()
        {
            Biomes = new Biomes();
            SmallPlant = new SmallPlant();
        }
    }
}
