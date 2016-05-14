using System;

using Xamarin.Forms;

namespace TestApp.Pages
{
    public class HomePage : ContentPage
    {
        
        private Button GoToRatingBarButton;
        private Button GoToCheckboxButton;

        public HomePage()
        {
            Title = "Messier16 Controls";

            GoToRatingBarButton = new Button();
            GoToRatingBarButton.Text = "RatingBar";
            GoToRatingBarButton.Clicked += GoToButton_Clicked;


            GoToCheckboxButton = new Button();
            GoToCheckboxButton.Text = "Checkbox";
            GoToCheckboxButton.Clicked += GoToButton_Clicked;

            Content = new StackLayout
            { 
                Children =
                {
                    GoToRatingBarButton,
                            GoToCheckboxButton
                }
            };
        }

        async void  GoToButton_Clicked (object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b == GoToRatingBarButton)
                await App.Navigation.PushAsync(new RatingBarPage());
            if (b == GoToCheckboxButton)
                await App.Navigation.PushAsync(new CheckboxPage());
        }
    }
}


