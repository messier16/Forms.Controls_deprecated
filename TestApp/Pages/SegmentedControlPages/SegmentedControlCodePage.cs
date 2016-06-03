using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.SegmentedControlPages
{
    public class SegmentedControlCodePage : ContentPage
    {
        public SegmentedControlCodePage()
        {

            var segmentedControl = new SegmentedControl();
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 1" });
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 2" });
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 3" });

            var grid = new Grid
            {
            };
            

            grid.Children.Add(segmentedControl);


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    grid
                }
            };
        }
    }
}


