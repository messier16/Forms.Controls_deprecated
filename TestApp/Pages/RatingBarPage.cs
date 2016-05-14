using System;

using Xamarin.Forms;
using TestApp.Pages.RatingBarPages;

namespace TestApp.Pages
{
    public class RatingBarPage : TabbedPage
    {
        public RatingBarPage()
        {
            Title = "RatingBar";

            Children.Add(new RatingBarCodePage { Title = "Code", Icon="code.png" });
            Children.Add(new RatingBarXamlPage { Title = "Markup", Icon = "xaml.png" });
        }
    }
}


