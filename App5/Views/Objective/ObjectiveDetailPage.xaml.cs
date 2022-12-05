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
    public partial class ObjectiveDetailPage : ContentPage
    {
        public Models.Objective Objective { get; set; }
        public ObjectiveDetailPage()
        {
            InitializeComponent();
            BindingContext = new ObjectiveDetailViewModel();
        }
    }
}