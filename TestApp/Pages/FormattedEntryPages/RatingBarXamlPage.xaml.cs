using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Pages.FormattedEntryPages
{
    public partial class FormattedEntryXamlPage : ContentPage
    {
        public FormattedEntryXamlPage()
        {
            BindingContext = new RatingBarViewModel();
            InitializeComponent();
        }
    }
}
