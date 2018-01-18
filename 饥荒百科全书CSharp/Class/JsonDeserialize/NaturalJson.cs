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

    public class SmallPlants
    {
        public List<NatureSmallPlant> NatureSmallPlant { get; set; }

        public SmallPlants()
        {
            NatureSmallPlant = new List<NatureSmallPlant>();
        }
    }

    public class NatureTree
    {
        public string Picture { get; set; }
        public List<string> Pictures { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string CutDownTools { get; set; }
        public List<List<string>> Resources { get; set; }
        public List<List<string>> ResourcesBurned { get; set; }
        public List<string> Ability { get; set; }
        public bool IsRegenerate { get; set; }
        public bool IsCombustible { get; set; }
        public List<string> Biomes { get; set; }
        public string Introduction { get; set; }
        public List<string> Console { get; set; }

        public NatureTree()
        {
            Pictures = new List<string>();
            Resources = new List<List<string>>();
            ResourcesBurned = new List<List<string>>();
            Ability = new List<string>();
            Biomes = new List<string>();
            Console = new List<string>();
        }
    }

    public class Trees
    {
        public List<NatureTree> NatureTree { get; set; }

        public Trees()
        {
            NatureTree = new List<NatureTree>();
        }
    }

    public class NatureCreatureNest
    {
        public string Picture { get; set; }
        public List<string> Pictures { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public List<int?> Health { get; set; }
        public List<List<string>> Creature { get; set; }
        public List<List<string>> ResourcesDestroyed { get; set; }
        public List<List<string>> Ability { get; set; }
        public bool IsRegenerate { get; set; }
        public bool IsDestroable { get; set; }
        public List<string> Biomes { get; set; }
        public string Introduction { get; set; }
        public List<string> Console { get; set; }

        public NatureCreatureNest()
        {
            Pictures = new List<string>();
            Creature = new List<List<string>>();
            ResourcesDestroyed = new List<List<string>>();
            Ability = new List<List<string>>();
            Biomes = new List<string>();
            Console = new List<string>();
        }
    }

    public class CreatureNests
    {
        public List<NatureCreatureNest> NatureCreatureNest { get; set; }

        public CreatureNests()
        {
            NatureCreatureNest = new List<NatureCreatureNest>();
        }
    }

    public class NaturalRootObject
    {
        public Biomes Biomes { get; set; }
        public SmallPlants SmallPlants { get; set; }
        public Trees Trees { get; set; }
        public CreatureNests CreatureNests { get; set; }

        public NaturalRootObject()
        {
            Biomes = new Biomes();
            SmallPlants = new SmallPlants();
            Trees = new Trees();
            CreatureNests = new CreatureNests();
        }
    }
}
