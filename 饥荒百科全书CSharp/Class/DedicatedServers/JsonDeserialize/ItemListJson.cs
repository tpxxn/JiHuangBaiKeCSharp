using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.JsonDeserialize
{
    public class Detail3
    {
        public string Chinese { get; set; }
        public string Code { get; set; }
    }

    public class Other
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Other()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Food
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Food()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Resources
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Resources()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Tools
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Tools()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Weapons
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Weapons()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Gifts
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Gifts()
        {
            Details = new List<Detail3>();
        }
    }
    
    public class Clothes
    {
        public string Chinese { get; set; }
        public List<Detail3> Details { get; set; }

        public Clothes()
        {
            Details = new List<Detail3>();
        }
    }

    public class Items
    {
        public Other Other { get; set; }
        public Food Food { get; set; }
        public Resources Resources { get; set; }
        public Tools Tools { get; set; }
        public Weapons Weapons { get; set; }
        public Gifts Gifts { get; set; }
        public Clothes Clothes { get; set; }

        public Items()
        {
            Other = new Other();
            Food = new Food();
            Resources = new Resources();
            Tools = new Tools();
            Weapons = new Weapons();
            Gifts = new Gifts();
            Clothes = new Clothes();
        }
    }

    public class ItemListRootObject
    {
        public Items Items { get; set; }

        public ItemListRootObject()
        {
            Items = new Items();
        }
    }
}
