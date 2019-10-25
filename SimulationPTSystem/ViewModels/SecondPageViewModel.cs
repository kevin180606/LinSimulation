using GalaSoft.MvvmLight.Command;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimulationPTSystem.ViewModels
{
    public class SecondPageViewModel : ModernViewModelBase
    {
        public ICommand NavigateToThirdPageCommand { get; }

        public SecondPageViewModel(IModernNavigationService navigationService) : base(navigationService) 
        {

            NavigateToThirdPageCommand = new RelayCommand(()=>
            {
                _navigationService.NavigateTo("ThirdPage");
            }); 
        }
    }
}
