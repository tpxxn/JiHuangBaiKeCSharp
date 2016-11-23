using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowXML处理
    /// </summary>
    public partial class MainWindow : Window
    {
        //加载游戏版本XML文件
        private void LoadGameVersionXml()
        {
            XmlDocument doc = new XmlDocument();
            Assembly assembly = Assembly.GetEntryAssembly();
            switch (UI_gameversion.SelectedIndex)
            {
                case 0:
                    Stream streamDS = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.DSXml.xml");
                    doc.Load(streamDS);
                    XmlNode listDS = doc.SelectSingleNode("DS");
                    HandleXml(listDS);
                    break;
                case 1:
                    Stream streamRoG = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.RoGXml.xml");
                    doc.Load(streamRoG);
                    XmlNode listRoG = doc.SelectSingleNode("RoG");
                    HandleXml(listRoG);
                    break;
                case 2:
                    Stream streamSW = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.SWXml.xml");
                    doc.Load(streamSW);
                    XmlNode listSW = doc.SelectSingleNode("SW");
                    HandleXml(listSW);
                    break;
                case 3:
                    Stream streamDST = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.DSTXml.xml");
                    doc.Load(streamDST);
                    XmlNode listDST = doc.SelectSingleNode("DST");
                    HandleXml(listDST);
                    break;
                case 4:
                    Stream streamTencent = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.TencentXml.xml");
                    doc.Load(streamTencent);
                    XmlNode listTencent = doc.SelectSingleNode("Tencent");
                    HandleXml(listTencent);
                    break;
            }
        }

        //处理XML
        private void HandleXml(XmlNode list)
        {
            if (WrapPanel_Left_Character != null)
            {
                WrapPanel_Left_Character.Children.Clear();
            }
            if (WrapPanel_Right_Character != null)
            {
                WrapPanel_Right_Character.Children.Clear();
            }
            if (WrapPanel_Left_Food != null)
            {
                WrapPanel_Left_Food.Children.Clear();
            }
            if (WrapPanel_Right_Food != null)
            {
                WrapPanel_Right_Food.Children.Clear();
            }
            foreach (XmlNode Node in list)
            {
                #region "人物"
                if (Node.Name == "CharacterNode")
                {
                    foreach (XmlNode childNode in Node)
                    {
                        if (childNode.Name == "Character")
                        {
                            string Picture = "";
                            string Name = "";
                            string EnName = "";
                            string Motto = "";
                            string Descriptions_1 = "";
                            string Descriptions_2 = "";
                            string Descriptions_3 = "";
                            string Health = "";
                            string Hunger = "";
                            string Sanity = "";
                            string LogMeter = "";
                            string Damage = "";
                            string DamageDay = "";
                            string DamageDusk = "";
                            string DamageNight = "";
                            string Introduce = "";
                            foreach (XmlNode Character in childNode)
                            {
                                switch (Character.Name)
                                {
                                    case "Picture":
                                        Picture = RSN.ShortName(Character.InnerText, "GameResources/Character/");
                                        break;
                                    case "Name":
                                        Name = Character.InnerText;
                                        break;
                                    case "EnName":
                                        EnName = Character.InnerText;
                                        break;
                                    case "Motto":
                                        Motto = Character.InnerText;
                                        break;
                                    case "Descriptions_1":
                                        Descriptions_1 = Character.InnerText;
                                        break;
                                    case "Descriptions_2":
                                        Descriptions_2 = Character.InnerText;
                                        break;
                                    case "Descriptions_3":
                                        Descriptions_3 = Character.InnerText;
                                        break;
                                    case "Health":
                                        Health = Character.InnerText;
                                        break;
                                    case "Hunger":
                                        Hunger = Character.InnerText;
                                        break;
                                    case "Sanity":
                                        Sanity = Character.InnerText;
                                        break;
                                    case "LogMeter":
                                        LogMeter = Character.InnerText;
                                        break;
                                    case "Damage":
                                        Damage = Character.InnerText;
                                        break;
                                    case "DamageDay":
                                        DamageDay = Character.InnerText;
                                        break;
                                    case "DamageDusk":
                                        DamageDusk = Character.InnerText;
                                        break;
                                    case "DamageNight":
                                        DamageNight = Character.InnerText;
                                        break;
                                    case "Introduce":
                                        Introduce = Character.InnerText;
                                        break;
                                }
                            }
                            ButtonWithText BWT = new ButtonWithText();
                            BWT.Height = 205;
                            BWT.Width = 140;
                            BWT.ButtonGrid.Height = 190;
                            BWT.ButtonGrid.Width = 140;
                            BWT.GridPictureHeight.Height = new GridLength(160);
                            BWT.UCImage.Height = 160;
                            BWT.UCImage.Width = 140;
                            BWT.UCImage.Source = RSN.PictureShortName(Picture);
                            BWT.UCTextBlock.FontSize = 20;
                            BWT.UCTextBlock.Text = Name;
                            string[] BWTTag = { Picture, Name, EnName, Motto, Descriptions_1, Descriptions_2, Descriptions_3, Health, Hunger, Sanity, LogMeter, Damage, DamageDay, DamageDusk, DamageNight, Introduce };
                            object obj = BWTTag;
                            if (Name == "威尔逊")
                            {
                                Character_Click_Handle(BWTTag);
                            }
                            BWT.UCButton.Tag = obj;
                            BWT.UCButton.Click += Character_Click;
                            try
                            {
                                WrapPanel_Right_Character.Children.Add(BWT);
                            }
                            catch { }
                        }
                    }
                }
                #endregion
                #region "食物"
                if (Node.Name == "FoodNode")
                {
                    foreach (XmlNode childNode in Node)
                    {
                        switch (childNode.Name)
                        {
                            #region "食谱"
                            case "FoodRecipe":
                                ExpanderStackpanel ESRecipe = new ExpanderStackpanel("食谱", "../Resources/CP_CrockPot.png");
                                WrapPanel_Right_Food.Children.Add(ESRecipe);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string PortableCrockPot = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Cooktime = "";
                                        string Priority = "";
                                        string NeedPicture_1 = "";
                                        string Need_1 = "";
                                        string NeedPicture_or = "";
                                        string Need_or = "";
                                        string NeedPicture_2 = "";
                                        string Need_2 = "";
                                        string NeedPicture_3 = "";
                                        string Need_3 = "";
                                        string Restrictions_1 = "";
                                        string RestrictionsAttributes_1 = "";
                                        string Restrictions_2 = "";
                                        string RestrictionsAttributes_2= "";
                                        string Restrictions_3 = "";
                                        string RestrictionsAttributes_3 = "";
                                        string Restrictions_4 = "";
                                        string RestrictionsAttributes_4 = "";
                                        string Restrictions_5 = "";
                                        string RestrictionsAttributes_5 = "";
                                        string Recommend_1 = "";
                                        string Recommend_2 = "";
                                        string Recommend_3 = "";
                                        string Recommend_4 = "";
                                        string Introduce = "";
                                        foreach (XmlNode Food in Level2childNode)
                                        {
                                            switch (Food.Name)
                                            {
                                                case "Picture":
                                                    Picture = RSN.ShortName(Food.InnerText, "GameResources/Food/");
                                                    break;
                                                case "Name":
                                                    Name = Food.InnerText;
                                                    break;
                                                case "EnName":
                                                    EnName = Food.InnerText;
                                                    break;
                                                case "PortableCrockPot":
                                                    PortableCrockPot = Food.InnerText;
                                                    break;
                                                case "Health":
                                                    Health = Food.InnerText;
                                                    break;
                                                case "Hunger":
                                                    Hunger = Food.InnerText;
                                                    break;
                                                case "Sanity":
                                                    Sanity = Food.InnerText;
                                                    break;
                                                case "Perish":
                                                    Perish = Food.InnerText;
                                                    break;
                                                case "Cooktime":
                                                    Cooktime = Food.InnerText;
                                                    break;
                                                case "Priority":
                                                    Priority = Food.InnerText;
                                                    break;
                                                case "NeedPicture_1":
                                                    NeedPicture_1 = Food.InnerText;
                                                    break;
                                                case "Need_1":
                                                    Need_1 = Food.InnerText;
                                                    break;
                                                case "NeedPicture_or":
                                                    NeedPicture_or = Food.InnerText;
                                                    break;
                                                case "Need_or":
                                                    Need_or = Food.InnerText;
                                                    break;
                                                case "NeedPicture_2":
                                                    NeedPicture_2 = Food.InnerText;
                                                    break;
                                                case "Need_2":
                                                    Need_2 = Food.InnerText;
                                                    break;
                                                case "NeedPicture_3":
                                                    NeedPicture_3 = Food.InnerText;
                                                    break;
                                                case "Need_3":
                                                    Need_3 = Food.InnerText;
                                                    break;
                                                case "Restrictions_1":
                                                    Restrictions_1 = Food.InnerText;
                                                    if (Food.Attributes["pre"] != null)
                                                    {
                                                        RestrictionsAttributes_1 = Food.Attributes["pre"].Value;
                                                    }
                                                    break;
                                                case "Restrictions_2":
                                                    Restrictions_2 = Food.InnerText;
                                                    if (Food.Attributes["pre"] != null)
                                                    {
                                                        RestrictionsAttributes_2 = Food.Attributes["pre"].Value;
                                                    }
                                                    break;
                                                case "Restrictions_3":
                                                    Restrictions_3 = Food.InnerText;
                                                    if (Food.Attributes["pre"] != null)
                                                    {
                                                        RestrictionsAttributes_3 = Food.Attributes["pre"].Value;
                                                    }
                                                    break;
                                                case "Restrictions_4":
                                                    Restrictions_4 = Food.InnerText;
                                                    if (Food.Attributes["pre"] != null)
                                                    {
                                                        RestrictionsAttributes_4 = Food.Attributes["pre"].Value;
                                                    }
                                                    break;
                                                case "Restrictions_5":
                                                    Restrictions_5 = Food.InnerText;
                                                    if (Food.Attributes["pre"] != null)
                                                    {
                                                        RestrictionsAttributes_5 = Food.Attributes["pre"].Value;
                                                    }
                                                    break;
                                                case "Recommend_1":
                                                    Recommend_1 = Food.InnerText;
                                                    break;
                                                case "Recommend_2":
                                                    Recommend_2 = Food.InnerText;
                                                    break;
                                                case "Recommend_3":
                                                    Recommend_3 = Food.InnerText;
                                                    break;
                                                case "Recommend_4":
                                                    Recommend_4 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, PortableCrockPot, Health, Hunger, Sanity, Perish, Cooktime, Priority, NeedPicture_1, Need_1, NeedPicture_or, Need_or, NeedPicture_2, Need_2, NeedPicture_3, Need_3, Restrictions_1, RestrictionsAttributes_1, Restrictions_2, RestrictionsAttributes_2, Restrictions_3, RestrictionsAttributes_3, Restrictions_4, RestrictionsAttributes_4, Restrictions_5, RestrictionsAttributes_5, Recommend_1, Recommend_2, Recommend_3, Recommend_4, Introduce };
                                        object obj = BWTTag;
                                        if (Name == "培根煎蛋")
                                        {
                                            Food_Click_Handle(BWTTag);
                                        }
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESRecipe.UCWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                break;
                            #endregion
                            #region "肉类"
                            case "FoodMeats":
                                ExpanderStackpanel ESMeast = new ExpanderStackpanel("肉类", "../Resources/GameResources/Food/FC_Meats.png");
                                WrapPanel_Right_Food.Children.Add(ESMeast);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Introduce = "";
                                        foreach (XmlNode Food in Level2childNode)
                                        {
                                            if (Food.Name == "Picture")
                                            {
                                                Picture = RSN.ShortName(Food.InnerText, "GameResources/Food/");
                                            }
                                            if (Food.Name == "Name")
                                            {
                                                Name = Food.InnerText;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        ESMeast.UCWrapPanel.Children.Add(BWT);
                                    }
                                }
                                break;
                            #endregion
                            #region "蔬菜"
                            case "FoodVegetables":
                                ExpanderStackpanel ESVegetables = new ExpanderStackpanel("蔬菜", "../Resources/GameResources/Food/FC_Vegetables.png");
                                WrapPanel_Right_Food.Children.Add(ESVegetables);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Introduce = "";
                                        foreach (XmlNode Food in Level2childNode)
                                        {
                                            if (Food.Name == "Picture")
                                            {
                                                Picture = RSN.ShortName(Food.InnerText, "GameResources/Food/");
                                            }
                                            if (Food.Name == "Name")
                                            {
                                                Name = Food.InnerText;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        ESVegetables.UCWrapPanel.Children.Add(BWT);
                                    }
                                }
                                break;
                                #endregion
                        }
                    }
                }
                #endregion
            }
        }
    }
}
