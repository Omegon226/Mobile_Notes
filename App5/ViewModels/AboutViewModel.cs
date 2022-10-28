using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App5.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Инфо";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/Omegon226/Mobile_Notes"));
        }

        public ICommand OpenWebCommand { get; }
    }
}