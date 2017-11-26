using System.Collections.Generic;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{

    public class Restrictions1
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions2
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions3
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions4
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class Restrictions5
    {
        public string Pre { get; set; }
        public string Text { get; set; }
    }

    public class FoodRecipe2
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public bool PortableCrockPot { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Temperature { get; set; }
        public double TemperatureDuration { get; set; }
        public double Perish { get; set; }
        public double Cooktime { get; set; }
        public double Priority { get; set; }
        public string NeedPicture1 { get; set; }
        public string Need1 { get; set; }
        public string NeedPicture2 { get; set; }
        public string Need2 { get; set; }
        public Restrictions1 Restrictions1 { get; set; }
        public Restrictions2 Restrictions2 { get; set; }
        public Restrictions3 Restrictions3 { get; set; }
        public Restrictions4 Restrictions4 { get; set; }
        public Restrictions5 Restrictions5 { get; set; }
        public string Recommend1 { get; set; }
        public string Recommend2 { get; set; }
        public string Recommend3 { get; set; }
        public string Recommend4 { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
        public string NeedPictureOr { get; set; }
        public string NeedOr { get; set; }
        public string NeedPicture3 { get; set; }
        public string Need3 { get; set; }

        public FoodRecipe2()
        {
            Restrictions1 = new Restrictions1();
            Restrictions2 = new Restrictions2();
            Restrictions3 = new Restrictions3();
            Restrictions4 = new Restrictions4();
            Restrictions5 = new Restrictions5();
        }
    }

    public class FoodRecipe
    {
        public List<FoodRecipe2> FoodRecipes { get; set; }

        public FoodRecipe()
        {
            FoodRecipes = new List<FoodRecipe2>();
        }
    }

    public class Food
    {
        public string Picture { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public double Health { get; set; }
        public double Hunger { get; set; }
        public double Sanity { get; set; }
        public double Perish { get; set; }
        public string Attribute { get; set; }
        public string AttributeValue { get; set; }
        public string Attribute2 { get; set; }
        public string AttributeValue2 { get; set; }
        public string Introduce { get; set; }
        public string Console { get; set; }
    }
   
    public class FoodMeats
    {
        public List<Food> Foods { get; set; }

        public FoodMeats()
        {
            Foods = new List<Food>();
        }
    }

    public class FoodVegetables
    {
        public List<Food> Foods { get; set; }

        public FoodVegetables()
        {
            Foods = new List<Food>();
        }
    }


    public class FoodFruits
    {
        public List<Food> Foods { get; set; }

        public FoodFruits()
        {
            Foods = new List<Food>();
        }
    }

    public class FoodEggs
    {
        public List<Food> Foods { get; set; }

        public FoodEggs()
        {
            Foods = new List<Food>();
        }
    }

    public class FoodOthers
    {
        public List<Food> Foods { get; set; }

        public FoodOthers()
        {
            Foods = new List<Food>();
        }
    }
    
    public class FoodNoFc
    {
        public List<Food> Foods { get; set; }

        public FoodNoFc()
        {
            Foods = new List<Food>();
        }
    }

    public class FoodRootObject
    {
        public FoodRecipe FoodRecipe { get; set; }
        public FoodMeats FoodMeats { get; set; }
        public FoodVegetables FoodVegetables { get; set; }
        public FoodFruits FoodFruit { get; set; }
        public FoodEggs FoodEggs { get; set; }
        public FoodOthers FoodOthers { get; set; }
        public FoodNoFc FoodNoFc { get; set; }

        public FoodRootObject()
        {
            FoodRecipe = new FoodRecipe();
            FoodMeats = new FoodMeats();
            FoodVegetables = new FoodVegetables();
            FoodFruit = new FoodFruits();
            FoodEggs = new FoodEggs();
            FoodOthers = new FoodOthers();
            FoodNoFc = new FoodNoFc();
        }
    }
}
