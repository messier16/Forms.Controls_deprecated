using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.SegmentedControlPages
{
    public class SegmentedControlCodePage : ContentPage
    {
        public SegmentedControlCodePage()
        {

            var ratingBar1 = new RatingBar();
            ratingBar1.FilledImage = "star_filled.png";
            ratingBar1.Image = "star.png";
            ratingBar1.MaxRating = 10;
            ratingBar1.HeightRequest = 50;

            var ratingBar2 = new RatingBar();
            ratingBar2.FilledImage = "star_filled.png";
            ratingBar2.Image = "star.png";
            ratingBar2.MaxRating = 5;
            ratingBar2.HeightRequest = 50;

            var ratingBeer = new RatingBar();
            ratingBeer.FilledImage = "beer_filled.png";
            ratingBeer.Image = "beer.png";
            ratingBeer.MaxRating = 5;
            ratingBeer.HeightRequest = 50;


            var rating2Label = new Label
            {
                Text = ratingBar2.Rating.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };


            var ratingBeerLabel = new Label
            {
                Text = ratingBeer.Rating + " beer",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            ratingBar2.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Rating"))
                    rating2Label.Text = ratingBar2.Rating.ToString();
            };

            ratingBeer.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Rating"))
                    ratingBeerLabel.Text = ratingBeer.Rating + " beer";
            };

            var grid = new Grid
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = 
                {
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)}
                }
            };
            

            grid.Children.Add(ratingBar2, 0, 0);
            grid.Children.Add(rating2Label, 1, 0);
            grid.Children.Add(ratingBeer, 0, 1);
            grid.Children.Add(ratingBeerLabel, 0, 2);
            Grid.SetColumnSpan(ratingBeer, 2);
            Grid.SetColumnSpan(ratingBeerLabel, 2);


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    ratingBar1,
                    grid
                }
            };
        }
    }
}


