using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.CheckboxPages
{
    public class CheckboxCodePage : ContentPage
    {
        public CheckboxCodePage()
        {

            var check = new Checkbox();
            Content = new StackLayout
            { 
                Children =
                {
                    check
                }
            };
        }
    }
}


