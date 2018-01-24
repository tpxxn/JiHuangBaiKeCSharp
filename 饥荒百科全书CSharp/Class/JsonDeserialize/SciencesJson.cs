using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{
    public class Science
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Need1 { get; set; }
        public int Need1Value { get; set; }
        public string Need2 { get; set; }
        public int Need2Value { get; set; }
        public string Need3 { get; set; }
        public int Need3Value { get; set; }
        public List<string> Unlock { get; set; }
        public string UnlockCharcter { get; set; }
        public string UnlockBlueprint { get; set; }
        public string Introduction { get; set; }
        public string ConsoleCommand { get; set; }
        public string Console { get; set; }

        public Science()
        {
            Unlock = new List<string>();
        }
    }

    public class Tool
    {
        public List<Science> Science { get; set; }

        public Tool()
        {
            Science = new List<Science>();
        }
    }

    public class Light
    {
        public List<Science> Science { get; set; }

        public Light()
        {
            Science = new List<Science>();
        }
    }

    public class Nautical
    {
        public List<Science> Science { get; set; }

        public Nautical()
        {
            Science = new List<Science>();
        }
    }

    public class Survival
    {
        public List<Science> Science { get; set; }

        public Survival()
        {
            Science = new List<Science>();
        }
    }

    public class Foods
    {
        public List<Science> Science { get; set; }

        public Foods()
        {
            Science = new List<Science>();
        }
    }

    public class Technology
    {
        public List<Science> Science { get; set; }

        public Technology()
        {
            Science = new List<Science>();
        }
    }

    public class Fight
    {
        public List<Science> Science { get; set; }

        public Fight()
        {
            Science = new List<Science>();
        }
    }

    public class Structure
    {
        public List<Science> Science { get; set; }

        public Structure()
        {
            Science = new List<Science>();
        }
    }

    public class Refine
    {
        public List<Science> Science { get; set; }

        public Refine()
        {
            Science = new List<Science>();
        }
    }

    public class Magic
    {
        public List<Science> Science { get; set; }

        public Magic()
        {
            Science = new List<Science>();
        }
    }

    public class Dress
    {
        public List<Science> Science { get; set; }

        public Dress()
        {
            Science = new List<Science>();
        }
    }

    public class Ancient
    {
        public List<Science> Science { get; set; }

        public Ancient()
        {
            Science = new List<Science>();
        }
    }

    public class Book
    {
        public List<Science> Science { get; set; }

        public Book()
        {
            Science = new List<Science>();
        }
    }

    public class Shadow
    {
        public List<Science> Science { get; set; }

        public Shadow()
        {
            Science = new List<Science>();
        }
    }

    public class Critter
    {
        public List<Science> Science { get; set; }

        public Critter()
        {
            Science = new List<Science>();
        }
    }

    public class Sculpt
    {
        public List<Science> Science { get; set; }

        public Sculpt()
        {
            Science = new List<Science>();
        }
    }

    public class Cartography
    {
        public List<Science> Science { get; set; }

        public Cartography()
        {
            Science = new List<Science>();
        }
    }

    public class Offerings
    {
        public List<Science> Science { get; set; }

        public Offerings()
        {
            Science = new List<Science>();
        }
    }

    
    public class Volcano
    {
        public List<Science> Science { get; set; }

        public Volcano()
        {
            Science = new List<Science>();
        }
    }

    public class ScienceRootObject
    {
        public Tool Tool { get; set; }
        public Light Light { get; set; }
        public Nautical Nautical { get; set; }
        public Survival Survival { get; set; }
        public Foods Foods { get; set; }
        public Technology Technology { get; set; }
        public Fight Fight { get; set; }
        public Structure Structure { get; set; }
        public Refine Refine { get; set; }
        public Magic Magic { get; set; }
        public Dress Dress { get; set; }
        public Ancient Ancient { get; set; }
        public Book Book { get; set; }
        public Shadow Shadow { get; set; }
        public Critter Critter { get; set; }
        public Sculpt Sculpt { get; set; }
        public Cartography Cartography { get; set; }
        public Offerings Offerings { get; set; }
        public Volcano Volcano { get; set; }

        public ScienceRootObject()
        {
            Tool = new Tool();
            Light = new Light();
            Nautical = new Nautical();
            Survival = new Survival();
            Foods = new Foods();
            Technology = new Technology();
            Fight = new Fight();
            Structure = new Structure();
            Refine = new Refine();
            Magic = new Magic();
            Dress = new Dress();
            Ancient = new Ancient();
            Book = new Book();
            Shadow = new Shadow();
            Critter = new Critter();
            Sculpt = new Sculpt();
            Cartography = new Cartography();
            Offerings = new Offerings();
            Volcano = new Volcano();
        }
    }
}
