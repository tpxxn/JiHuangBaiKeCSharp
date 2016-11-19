using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowXml处理
    /// </summary>
    public partial class MainWindow : Window
    {
        //加载游戏版本Xml文件
        private void LoadGameVersionXml()
        {
            XmlDocument doc = new XmlDocument();
            Assembly assembly = Assembly.GetEntryAssembly();
            switch (UI_gameversion.SelectedIndex)
            {
                case 0:
                    Stream streamDS = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.DSXml.xml");
                    doc.Load(streamDS);
                    XmlNode listDS = doc.SelectSingleNode("DS");
                    HandleXml(listDS);
                    break;
                case 1:
                    Stream streamRoG = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.RoGXml.xml");
                    doc.Load(streamRoG);
                    XmlNode listRoG = doc.SelectSingleNode("RoG");
                    HandleXml(listRoG);
                    break;
                case 2:
                    Stream streamSW = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.SWXml.xml");
                    doc.Load(streamSW);
                    XmlNode listSW = doc.SelectSingleNode("SW");
                    HandleXml(listSW);
                    break;
                case 3:
                    Stream streamDST = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.DSTXml.xml");
                    doc.Load(streamDST);
                    XmlNode listDST = doc.SelectSingleNode("DST");
                    HandleXml(listDST);
                    break;
                case 4:
                    Stream streamTencent = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.TencentXml.xml");
                    doc.Load(streamTencent);
                    XmlNode listTencent = doc.SelectSingleNode("Tencent");
                    HandleXml(listTencent);
                    break;
            }
        }

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
            foreach (XmlNode Node in list)
            {
                #region "人物"
                if (Node.Name == "CharacterNode")
                {
                    //messagebox.show(node.attributes["name"].value);
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
                                        Picture = Character.InnerText;
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
                        if (childNode.Name == "Food")
                        {
                            string Picture = "";
                            string Name = "";
                            string EnName = "";
                            string Introduce = "";
                            foreach (XmlNode Food in childNode)
                            {
                                if (Food.Name == "Picture")
                                {
                                    Picture = Food.InnerText;
                                }
                                if (Food.Name == "Name")
                                {
                                    Name = Food.InnerText;
                                }
                            }
                            ButtonWithText BWT = new ButtonWithText();
                            BWT.UCImage.Source = RSN.PictureShortName(Picture);
                            BWT.UCTextBlock.Text = Name;
                            WrapPanel_Right_Food.Children.Add(BWT);
                        }
                    }
                }
                #endregion
            }
        }
    }
}
