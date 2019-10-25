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
using TaskConstructor.Views;

namespace SimulationPTSystem.Views
{
    /// <summary>
    /// TotalDataGridPage.xaml 的交互逻辑
    /// </summary>
    public partial class TotalDataGridPage : NavigationPage
    {
        public TotalDataGridPage()
        {
            InitializeComponent();
        }

        private void NavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (DarkFieldContext dfc = new DarkFieldContext())
            {
                this.ASPNET.ItemsSource = dfc.ItemBank.Select(o => o).ToList<ItemBank>();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion addWin = new AddQuestion();
            addWin.ShowDialog();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ASPNET.SelectedItems != null)
            {
                using (DarkFieldContext dfc = new DarkFieldContext())
                {
                    List<ItemBank> listSelected = new List<ItemBank>();

                    foreach (var item in this.ASPNET.SelectedItems)
                    {
                        ItemBank t = item as ItemBank;

                        if (t != null)
                        {
                            var person = dfc.ItemBank.Where(m => m.ItemBankId == t.ItemBankId).FirstOrDefault();
                            dfc.ItemBank.Remove(person);

                        }
                        //移除各个分区试题中的对应项 
                        foreach (var quesIndex in dfc.ZoneItemBank.ToList())
                        {
                            //    string[] temIndex = null;

                            var temIndex = quesIndex.QuesIndexes.Split(',').ToList();

                            for (int i = 0; i < temIndex.Count; i++)
                            {
                                if (t.ItemBankId.ToString() == temIndex[i])
                                {
                                    temIndex.RemoveAt(i);
                                    i--;
                                }

                            }


                            quesIndex.QuesIndexes = "";

                            for (int i = 0; i < temIndex.Count; i++)
                            {

                                if (quesIndex.QuesIndexes != "")
                                {
                                    quesIndex.QuesIndexes = quesIndex.QuesIndexes + "," + temIndex[i];
                                }
                                else
                                {
                                    quesIndex.QuesIndexes = temIndex[i];
                                }
                            }

                        };

                    }
                    dfc.SaveChanges();
                    this.ASPNET.ItemsSource = dfc.ItemBank.Select(o => o).ToList<ItemBank>();
                }
            }



        }

    }
}
