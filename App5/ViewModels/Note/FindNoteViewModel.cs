using App5.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App5.ViewModels.Note
{
    class FindNoteViewModel : BaseViewModel
    {
        private string name;
        public FindNoteViewModel()
        {
            FindCommand = new Command(OnCancel);
            CancelCommand = new Command(OnCancel);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public Command FindCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
