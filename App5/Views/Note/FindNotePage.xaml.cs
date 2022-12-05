using App5.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views.Note
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindNotePage : ContentPage
    {
        FindNoteViewModel _viewModel;
        public FindNotePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FindNoteViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}