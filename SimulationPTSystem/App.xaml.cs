using LinnerToolkit.Desktop.ModernUI.Windows;
using SimulationPTSystem.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimulationPTSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ModernApplication
    {
        public App()
        {
            RunApp("MainWindow/MainPage");
        }

        protected override void RegisterTypes()
        {
            // 注册导航窗体、页面
            RegisterForNavigation<MainWindow>();
            RegisterForNavigation<MainPage>();
            RegisterForNavigation<SecondPage>();
            RegisterForNavigation<ThirdPage>();
        }
    }
}
