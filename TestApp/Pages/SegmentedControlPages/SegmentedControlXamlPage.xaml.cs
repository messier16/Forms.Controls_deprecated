using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Pages.SegmentedControlPages
{
    public partial class SegmentedControlXamlPage : ContentPage
    {
        public SegmentedControlXamlPage()
        {
            BindingContext = new RatingBarViewModel();
            InitializeComponent();
        }
    }
}
