using System;

using Xamarin.Forms;
using TestApp.Pages.CheckboxPages;

namespace TestApp.Pages
{
    public class CheckboxPage : TabbedPage
    {
        public CheckboxPage()
        {
            Children.Add(new CheckboxCodePage { Title ="Code", Icon ="code.png"});
        }
    }
}


