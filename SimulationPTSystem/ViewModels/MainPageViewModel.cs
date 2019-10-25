using GalaSoft.MvvmLight.Command;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using SimulationPTSystem.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimulationPTSystem.ViewModels
{
    public class MainPageViewModel : ModernViewModelBase
    {
        protected IDataConverter _dataConverter;

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public ICommand NavigateToTotalPageCommand { get; }
        public ICommand NavigateToZonePageCommand { get; }
        

        public MainPageViewModel(IModernNavigationService navigationService):base(navigationService)
        {
            StartCommand = new RelayCommand(async () =>
              {
                  if (_dataConverter == null)
                  {
                      _dataConverter = new SimulationPTSystem.DataConverter.Screen.DataConverter();
                      _dataConverter.DataReceived += DataConverter_DataReceived;
                      await _dataConverter.StartAsync();
                  }
              });
            StopCommand = new RelayCommand(async () =>
              {
                  if (_dataConverter != null)
                  {
                      _dataConverter.DataReceived -= DataConverter_DataReceived;
                      await _dataConverter.StopAsync();
                      _dataConverter = null;
                  }
              });
            NavigateToTotalPageCommand = new RelayCommand(() =>
              {
                  _navigationService.NavigateTo("TotalDataGridPage");
              });
            NavigateToZonePageCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo("ZonesSelectionPage");
            });

            
        }
        private void DataConverter_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Console.WriteLine(BitConverter.ToString(e.Data));
            }
        }
    }
}
