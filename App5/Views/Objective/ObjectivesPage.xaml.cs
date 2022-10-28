using App5.Models;
using App5.ViewModels;
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

namespace App5.Views.Objective
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectivesPage : ContentPage
    {
        ObjectivesViewModel _viewModel;
        public ObjectivesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ObjectivesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}