using System;

using Xamarin.Forms;
using TestApp.Pages.FormattedEntryPages;

namespace TestApp.Pages
{
    public class FormattedEntryPage : TabbedPage
    {
        public FormattedEntryPage()
        {
            Title = "FormattedEntry";
			AutomationId = "page";

            Children.Add(new FormattedEntryCodePage { Title = "Code" , Icon="code.png" });
			Children.Add(new FormattedEntryXamlPage { Title = "Markup", Icon = "xaml.png" });
        }
    }
}


