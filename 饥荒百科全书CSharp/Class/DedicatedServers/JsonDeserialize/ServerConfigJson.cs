using System.Collections.Generic;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.JsonDeserialize
{
    public class Detail
    {
        public string English { get; set; }
        public string Chinese { get; set; }
    }

    public class Hanization
    {
        public List<Detail> Details { get; set; }

        public Hanization()
        {
            Details = new List<Detail>();
        }
    }

    public class Detail2
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Master
    {
        public List<Detail2> Details { get; set; }

        public Master()
        {
            Details = new List<Detail2>();
        }
    }
    
    public class Caves
    {
        public List<Detail2> Details { get; set; }

        public Caves()
        {
            Details = new List<Detail2>();
        }
    }
    
    public class Master2
    {
        public List<Detail2> Details { get; set; }

        public Master2()
        {
            Details = new List<Detail2>();
        }
    }
    
    public class Cave
    {
        public List<Detail2> Details { get; set; }

        public Cave()
        {
            Details = new List<Detail2>();
        }
    }

    public class Classification
    {
        public Master2 Master { get; set; }
        public Cave Cave { get; set; }

        public Classification()
        {
            Master = new Master2();
            Cave = new Cave();
        }
    }

    public class Configuration
    {
        public Hanization Hanization { get; set; }
        public Master Master { get; set; }
        public Caves Caves { get; set; }
        public Classification Classification { get; set; }

        public Configuration()
        {
            Hanization = new Hanization();
            Master = new Master();
            Caves = new Caves();
            Classification = new Classification();
        }
    }

    public class ServerConfigRootObject
    {
        public Configuration Configuration { get; set; }

        public ServerConfigRootObject()
        {
            Configuration = new Configuration();
        }
    }
}
