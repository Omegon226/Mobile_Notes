using App5.Models;
using App5.ViewModels;
using App5.ViewModels.Note;
using App5.ViewModels.Objective;
using App5.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views.Note
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        NotesViewModel _viewModel;
        public NotesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}