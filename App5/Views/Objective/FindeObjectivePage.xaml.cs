using App5.ViewModels.Objective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views.Objective
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindeObjectivePage : ContentPage
    {
        FindObjectiveViewModel _viewModel;
        public FindeObjectivePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FindObjectiveViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}