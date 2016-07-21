using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.FormattedEntryPages
{
    public class FormattedEntryCodePage : ContentPage
    {
        public FormattedEntryCodePage()
        {

			var formattedEntry1 = new FormattedNumberEntry();
			var formattedEntry2 = new FormattedNumberEntry();

            Content = new StackLayout
            {
				Padding = 20,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
					formattedEntry1,
					formattedEntry2
                }
            };
        }
    }
}


