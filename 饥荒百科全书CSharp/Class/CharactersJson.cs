using System.Collections.Generic;

namespace 饥荒百科全书CSharp.Class
{
    public class Character
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Motto { get; set; }
        public List<string> Descriptions { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Damage { get; set; }
        public string Unlock { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
        public int LogMeter { get; set; }
        public int DamageDay { get; set; }
        public int DamageDusk { get; set; }
        public int DamageNight { get; set; }
    }

    public class CharacterRootObject
    {
        public List<Character> Character { get; set; }

        public CharacterRootObject()
        {
            Character = new List<Character>();
        }
    }
}
