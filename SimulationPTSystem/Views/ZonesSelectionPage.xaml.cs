using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskConstructor.EFModels;

namespace SimulationPTSystem.Views
{
    /// <summary>
    /// SecondPage.xaml 的交互逻辑
    /// </summary>
    public partial class ZonesSelectionPage : NavigationPage
    {
        public ZonesSelectionPage()
        {
            InitializeComponent();
        }
        public string ExportString = "";
        List<ItemBank> listTemp = new List<ItemBank>();

        private void NavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (DarkFieldContext dfc = new DarkFieldContext())
            {
                this.ZoneWhone.ItemsSource = dfc.ItemBank.Select(o => o).ToList<ItemBank>();
                //    this.Zone1.ItemsSource = dfc.Questions.Where(o =>o.QuestionGenre =="Picture").ToList<Question>();
                listTemp = dfc.ItemBank.ToList<ItemBank>();
            }
        }
        private void zoneA_Click(object sender, RoutedEventArgs e)
        {
            this.currentZone.Text = "当前选中的区域为A";
            string[] sArray;
            List<int> listIndex = new List<int>();

            using (DarkFieldContext dfc = new DarkFieldContext())
            {

                ZoneItemBank zone1 = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone A").FirstOrDefault();

                if (zone1 != null)
                {
                    ExportString = zone1.QuesIndexes;
                    if (ExportString != string.Empty)
                    {
                        sArray = ExportString.Split(',');
                        foreach (string i in sArray)
                        {
                            listIndex.Add(int.Parse(i));
                        }
                        //顺序问题  

                        List<ItemBank> seqQuestions = new List<ItemBank>();
                        for (int i = 0; i < listIndex.Count(); i++)
                        {
                            seqQuestions.Add(listTemp.First(o => o.ItemBankId == listIndex[i]).Clone());
                        }

                        this.Zone1.ItemsSource = seqQuestions;
                    }
                    else
                    {
                        this.Zone1.ItemsSource = null;
                    }


                }
                else
                {
                    ZoneItemBank zoneAA = new ZoneItemBank();
                    zoneAA.ZoneCode = "Zone A";
                    zoneAA.QuesIndexes = "";
                    dfc.ZoneItemBank.Add(zoneAA);
                    dfc.SaveChanges();
                }











            }



        }
        private void zoneB_Click(object sender, RoutedEventArgs e)
        {
            this.currentZone.Text = "当前选中的区域为B";

            string[] sArray;
            List<int> listIndex = new List<int>();
            using (DarkFieldContext dfc = new DarkFieldContext())
            {
                ZoneItemBank zone1 = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone B").FirstOrDefault();

                if (zone1 != null)
                {
                    ExportString = zone1.QuesIndexes;
                    if (ExportString != String.Empty)
                    {
                        sArray = ExportString.Split(',');
                        foreach (string i in sArray)
                        {
                            listIndex.Add(int.Parse(i));
                        }
                        //顺序问题  

                        List<ItemBank> seqQuestions = new List<ItemBank>();
                        for (int i = 0; i < listIndex.Count(); i++)
                        {
                            seqQuestions.Add(listTemp.First(o => o.ItemBankId == listIndex[i]).Clone());
                        }

                        this.Zone1.ItemsSource = seqQuestions;
                    }
                    else
                    {
                        this.Zone1.ItemsSource = null;
                    }

                }
                else
                {
                    ZoneItemBank zoneAA = new ZoneItemBank();
                    zoneAA.ZoneCode = "Zone B";
                    zoneAA.QuesIndexes = "";
                    dfc.ZoneItemBank.Add(zoneAA);
                    dfc.SaveChanges();
                }

            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //  if (this.ZoneWhone.SelectedItems != null && this.Zone1.ItemsSource !=null)
            if (this.ZoneWhone.SelectedItems != null)
            {
                List<ItemBank> sourceList = new List<ItemBank>();
                List<ItemBank> destList = new List<ItemBank>();

                if (this.ZoneWhone.SelectedItems != null)
                {
                    sourceList = this.ZoneWhone.SelectedItems.OfType<ItemBank>().ToList();
                }
                if (this.Zone1.ItemsSource != null)
                {
                    destList = this.Zone1.ItemsSource.OfType<ItemBank>().ToList();
                }



                using (DarkFieldContext dfc = new DarkFieldContext())
                {
                    switch (this.currentZone.Text)
                    {
                        case "当前选中的区域为A":
                            ZoneItemBank zoneA = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone A").FirstOrDefault();
                            if (zoneA == null)
                            {
                                ZoneItemBank zoneAA = new ZoneItemBank();
                                zoneAA.ZoneCode = "Zone A";
                                zoneAA.QuesIndexes = "";
                                dfc.ZoneItemBank.Add(zoneAA);
                                dfc.SaveChanges();
                            }
                            zoneA = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone A").FirstOrDefault();

                            foreach (var item in sourceList)
                            {
                                ItemBank t = item as ItemBank;
                                if (t != null)
                                {
                                    destList.Add(t);

                                    if (zoneA.QuesIndexes != "")
                                    {
                                        zoneA.QuesIndexes = zoneA.QuesIndexes + "," + t.ItemBankId;
                                    }
                                    else
                                    {
                                        zoneA.QuesIndexes = t.ItemBankId.ToString();
                                    }
                                    // Zone1.ItemsSource = aList;
                                    //Zone1.ItemsSource
                                    //   this.Zone1.Items.RemoveAt(Zone1.SelectedIndex);
                                }
                            }
                            dfc.SaveChanges();
                            Zone1.ItemsSource = destList;
                            break;
                        case "当前选中的区域为B":
                            //  ZoneItemBank zoneB = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone B").First();

                            ZoneItemBank zoneB = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone B").FirstOrDefault();
                            if (zoneB == null)
                            {
                                ZoneItemBank zoneAA = new ZoneItemBank();
                                zoneAA.ZoneCode = "Zone B";
                                zoneAA.QuesIndexes = "";
                                dfc.ZoneItemBank.Add(zoneAA);
                                dfc.SaveChanges();
                            }
                            zoneB = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone B").FirstOrDefault();

                            foreach (var item in sourceList)
                            {
                                ItemBank t = item as ItemBank;
                                if (t != null)
                                {
                                    destList.Add(t);

                                    if (zoneB.QuesIndexes != "")
                                    {
                                        zoneB.QuesIndexes = zoneB.QuesIndexes + "," + t.ItemBankId;
                                    }
                                    else
                                    {
                                        zoneB.QuesIndexes = t.ItemBankId.ToString();
                                    }

                                }
                            }
                            dfc.SaveChanges();
                            Zone1.ItemsSource = destList;
                            break;

                    }
                }

            }




        }

        string ofoIds = string.Empty;

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            using (DarkFieldContext dfc = new DarkFieldContext())
            {
                switch (this.currentZone.Text)
                {
                    case "当前选中的区域为A":


                        List<ItemBank> aList = Zone1.ItemsSource.OfType<ItemBank>().ToList();
                        ZoneItemBank zone1 = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone A").First();


                        if (this.Zone1.SelectedItems != null)
                        {
                            foreach (var item in this.Zone1.SelectedItems.OfType<ItemBank>().ToList())
                            {
                                ItemBank t = item as ItemBank;
                                if (t != null)
                                {
                                    aList.Remove(t);
                                    Zone1.ItemsSource = aList;
                                    //Zone1.ItemsSource
                                    //   this.Zone1.Items.RemoveAt(Zone1.SelectedIndex);
                                }
                            }
                        }
                        else
                        {

                        }

                        if (aList.Count > 0)
                        {
                            ofoIds = "";
                            for (int i = 0; i < aList.Count(); i++)
                            {
                                ofoIds = ofoIds + aList[i].ItemBankId.ToString() + ",";
                            }

                            ofoIds = ofoIds.Remove(ofoIds.Length - 1, 1);
                            zone1.QuesIndexes = ofoIds;
                            dfc.SaveChanges();

                        }
                        else
                        {
                            zone1.QuesIndexes = "";
                            dfc.SaveChanges();
                        }

                        break;
                    case "当前选中的区域为B":

                        List<ItemBank> aListB = Zone1.ItemsSource.OfType<ItemBank>().ToList();
                        ZoneItemBank zoneB = dfc.ZoneItemBank.Where(o => o.ZoneCode == "Zone B").First();


                        if (this.Zone1.SelectedItems != null)
                        {
                            foreach (var item in this.Zone1.SelectedItems.OfType<ItemBank>().ToList())
                            {
                                ItemBank t = item as ItemBank;
                                if (t != null)
                                {
                                    aListB.Remove(t);
                                    Zone1.ItemsSource = aListB;
                                    //Zone1.ItemsSource
                                    //   this.Zone1.Items.RemoveAt(Zone1.SelectedIndex);
                                }
                            }
                        }
                        else
                        {

                        }

                        if (aListB.Count > 0)
                        {
                            ofoIds = "";
                            for (int i = 0; i < aListB.Count(); i++)
                            {
                                ofoIds = ofoIds + aListB[i].ItemBankId.ToString() + ",";
                            }

                            ofoIds = ofoIds.Remove(ofoIds.Length - 1, 1);
                            zoneB.QuesIndexes = ofoIds;
                            dfc.SaveChanges();

                        }
                        else
                        {
                            zoneB.QuesIndexes = "";
                            dfc.SaveChanges();
                        }
                        break;

                    default:
                        break;
                }
            }
        }


    }
}
