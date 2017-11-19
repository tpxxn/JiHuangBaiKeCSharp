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
            switch (UiGameversion.SelectedIndex)
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
            #region 初始化
            if (WrapPanelLeftFood != null)
            {
                WrapPanelLeftFood.Children.Clear();
            }
            if (WrapPanelRightFood != null)
            {
                WrapPanelRightFood.Children.Clear();
            }
            if (WrapPanelRightCookingSimulator != null)
            {
                WrapPanelRightCookingSimulator.Children.Clear();
            }
            #endregion
            foreach (XmlNode Node in list)
            {
                #region 食物&模拟
                if (Node.Name == "FoodNode")
                {
                    foreach (XmlNode childNode in Node)
                    {
                        switch (childNode.Name)
                        {
                            #region 食谱
                            case "FoodRecipe":
                                ExpanderStackpanel ESRecipe = new ExpanderStackpanel("食谱", "../Resources/CP_CrockPot.png");
                                WrapPanelRightFood.Children.Add(ESRecipe);
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
                                        string RestrictionsAttributes_2 = "";
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
                                            Food_Recipe_Click_Handle(BWTTag);
                                        }
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Recipe_Click;
                                        try
                                        {
                                            ESRecipe.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                break;
                            #endregion
                            #region 肉类
                            case "FoodMeats":
                                #region 食物
                                ExpanderStackpanel ESMeast = new ExpanderStackpanel("肉类", "../Resources/GameResources/Food/FC_Meats.png");
                                WrapPanelRightFood.Children.Add(ESMeast);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESMeast.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                #region 模拟
                                ExpanderStackpanel ESMeast_CS = new ExpanderStackpanel("肉类", "../Resources/GameResources/Food/FC_Meats.png");
                                WrapPanelRightCookingSimulator.Children.Add(ESMeast_CS);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Health":
                                                    Health = Food.InnerText;
                                                    break;
                                                case "Hunger":
                                                    Hunger = Food.InnerText;
                                                    break;
                                                case "Sanity":
                                                    Sanity = Food.InnerText;
                                                    break;
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, Health, Hunger, Sanity, Attribute, AttributeValue, Attribute_2, AttributeValue_2 };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += CookingSimulator_Click;
                                        try
                                        {
                                            ESMeast_CS.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            #region 蔬菜
                            case "FoodVegetables":
                                #region 食物
                                ExpanderStackpanel ESVegetables = new ExpanderStackpanel("蔬菜", "../Resources/GameResources/Food/FC_Vegetables.png");
                                WrapPanelRightFood.Children.Add(ESVegetables);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESVegetables.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                #region 模拟
                                ExpanderStackpanel ESVegetables_CS = new ExpanderStackpanel("蔬菜", "../Resources/GameResources/Food/FC_Vegetables.png");
                                WrapPanelRightCookingSimulator.Children.Add(ESVegetables_CS);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += CookingSimulator_Click;
                                        try
                                        {
                                            ESVegetables_CS.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            #region 水果
                            case "FoodFruit":
                                #region 食物
                                ExpanderStackpanel ESFruit = new ExpanderStackpanel("水果", "../Resources/GameResources/Food/FC_Fruit.png");
                                WrapPanelRightFood.Children.Add(ESFruit);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESFruit.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                #region 模拟
                                ExpanderStackpanel ESFruit_CS = new ExpanderStackpanel("水果", "../Resources/GameResources/Food/FC_Fruit.png");
                                WrapPanelRightCookingSimulator.Children.Add(ESFruit_CS);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += CookingSimulator_Click;
                                        try
                                        {
                                            ESFruit_CS.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            #region 蛋类
                            case "FoodEggs":
                                #region 食物
                                ExpanderStackpanel ESEggs = new ExpanderStackpanel("蛋类", "../Resources/GameResources/Food/FC_Eggs.png");
                                WrapPanelRightFood.Children.Add(ESEggs);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESEggs.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                #region 模拟
                                ExpanderStackpanel ESEggs_CS = new ExpanderStackpanel("蛋类", "../Resources/GameResources/Food/FC_Eggs.png");
                                WrapPanelRightCookingSimulator.Children.Add(ESEggs_CS);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += CookingSimulator_Click;
                                        try
                                        {
                                            ESEggs_CS.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            #region 其他
                            case "FoodOthers":
                                #region 食物
                                ExpanderStackpanel ESOthers = new ExpanderStackpanel("其他", "../Resources/GameResources/Food/F_twigs.png");
                                WrapPanelRightFood.Children.Add(ESOthers);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_Click;
                                        try
                                        {
                                            ESOthers.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                #region 模拟
                                ExpanderStackpanel ESOthers_CS = new ExpanderStackpanel("其他", "../Resources/GameResources/Food/F_twigs.png");
                                WrapPanelRightCookingSimulator.Children.Add(ESOthers_CS);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
                                        string AttributeValue = "";
                                        string Attribute_2 = "";
                                        string AttributeValue_2 = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "AttributeValue":
                                                    AttributeValue = Food.InnerText;
                                                    break;
                                                case "Attribute_2":
                                                    Attribute_2 = Food.InnerText;
                                                    break;
                                                case "AttributeValue_2":
                                                    AttributeValue_2 = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += CookingSimulator_Click;
                                        try
                                        {
                                            ESOthers_CS.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            #region 非食材
                            case "FoodNoFC":
                                ExpanderStackpanel ESNoFC = new ExpanderStackpanel("非食材", "../Resources/GameResources/Food/F_petals.png");
                                WrapPanelRightFood.Children.Add(ESNoFC);
                                foreach (XmlNode Level2childNode in childNode)
                                {
                                    if (Level2childNode.Name == "Food")
                                    {
                                        string Picture = "";
                                        string Name = "";
                                        string EnName = "";
                                        string Health = "";
                                        string Hunger = "";
                                        string Sanity = "";
                                        string Perish = "";
                                        string Attribute = "";
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
                                                case "Attribute":
                                                    Attribute = Food.InnerText;
                                                    break;
                                                case "Introduce":
                                                    Introduce = Food.InnerText;
                                                    break;
                                            }
                                        }
                                        ButtonWithText BWT = new ButtonWithText();
                                        BWT.UCImage.Source = RSN.PictureShortName(Picture);
                                        BWT.UCTextBlock.Text = Name;
                                        string[] BWTTag = { Picture, Name, EnName, Health, Hunger, Sanity, Perish, Attribute, Introduce };
                                        object obj = BWTTag;
                                        BWT.UCButton.Tag = obj;
                                        BWT.UCButton.Click += Food_NoFC_Click;
                                        try
                                        {
                                            ESNoFC.UcWrapPanel.Children.Add(BWT);
                                        }
                                        catch { }
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
