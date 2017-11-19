using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class
{
    public class Creature
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int Health { get; set; }
        public double Attack { get; set; }
        public double AttackInterval { get; set; }
        public double AttackScope { get; set; }
        public double MoveSpeed { get; set; }
        public double RunSpeed { get; set; }
        public int Dangerous { get; set; }
        public double SanityEffect { get; set; }
        public bool IsActiveAttack { get; set; }
        public bool IsTeamWork { get; set; }
        public List<string> Goods { get; set; }
        public List<string> Ability { get; set; }
        public string Introduction { get; set; }
        public string Console { get; set; }
        public string ConsoleStateValue { get; set; }

        public Creature()
        {
            Goods = new List<string>();
            Ability = new List<string>();
        }
    }

    public class Land
    {
        public List<Creature> Creature { get; set; }

        public Land()
        {
            Creature = new List<Creature>();
        }
    }
    
    public class Ocean
    {
        public List<Creature> Creature { get; set; }

        public Ocean()
        {
            Creature = new List<Creature>();
        }
    }

    public class Fly
    {
        public List<Creature> Creature { get; set; }

        public Fly()
        {
            Creature = new List<Creature>();
        }
    }
    
    public class Cave
    {
        public List<Creature> Creature { get; set; }

        public Cave()
        {
            Creature = new List<Creature>();
        }
    }
    
    public class Evil
    {
        public List<Creature> Creature { get; set; }

        public Evil()
        {
            Creature = new List<Creature>();
        }
    }
    
    public class Others
    {
        public List<Creature> Creature { get; set; }

        public Others()
        {
            Creature = new List<Creature>();
        }
    }
    
    public class Boss
    {
        public List<Creature> Creature { get; set; }

        public Boss()
        {
            Creature = new List<Creature>();
        }
    }

    public class CreaturesRootObject
    {
        public Land Land { get; set; }
        public Ocean Ocean { get; set; }
        public Fly Fly { get; set; }
        public Cave Cave { get; set; }
        public Evil Evil { get; set; }
        public Others Others { get; set; }
        public Boss Boss { get; set; }

        public CreaturesRootObject()
        {
            Land = new Land();
            Ocean = new Ocean();
            Fly = new Fly();
            Cave = new Cave();
            Evil = new Evil();
            Others = new Others();
            Boss = new Boss();
        }
    }
}
