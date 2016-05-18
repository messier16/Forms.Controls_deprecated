using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Messier16.Forms.Uwp.Controls.Native.RatingBar
{
    public sealed partial class RatingBar : UserControl
    {
        public RatingBar()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register(
            "RatingValue",
            typeof(Int32),
            typeof(RatingBar),
            new PropertyMetadata(5, InnerRatingValueChanged));

        public Int32 RatingValue
        {
            get
            {
                return (Int32)GetValue(RatingValueProperty);
            }
            set
            {
                if (value < 0)
                {
                    SetValue(RatingValueProperty, 0);
                }
                else if (value > NumberOfStars)
                {
                    SetValue(RatingValueProperty, NumberOfStars);
                }
                else
                {
                    SetValue(RatingValueProperty, value);
                }

                RatingValueChanged?.Invoke(this, new RatingValueChangedArgs(value));
            }
        }

        public event RatingValueChanged RatingValueChanged;

        private static void InnerRatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RatingBar parent = sender as RatingBar;
            Int32 ratingValue = (Int32)e.NewValue;
            UIElementCollection children = (parent.Content as StackPanel).Children;

            ToggleButton button = null;
            if (!children.Any())
                return;

            for (Int32 i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = true;
            }

            for (Int32 i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = false;
            }

        }

        public static readonly DependencyProperty NumberOfStarsProperty = DependencyProperty.Register(
            "NumberOfStars",
            typeof(Int32),
            typeof(RatingBar),
            new PropertyMetadata(0, new PropertyChangedCallback(NumberOfStarsValueChanged)));

        public Int32 NumberOfStars
        {
            get
            {
                return (Int32)GetValue(NumberOfStarsProperty);
            }
            set
            {
                SetValue(NumberOfStarsProperty, value);
            }
        }

        private static void NumberOfStarsValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RatingBar parent = sender as RatingBar;
            Int32 NumberOfStarsValue = (Int32)e.NewValue;

            for (int i = 1; i <= NumberOfStarsValue; i++)
            {
                var btn = new ToggleButton()
                {
                    Tag = i,
                    Height = parent.HeightValue,
                    Style = parent.Resources["StarButton"] as Style,
                    Foreground = parent.StarForegroundColor
                };
                btn.Click += parent.RatingButtonClickEventHandler;
                btn.PointerEntered += parent.ToggleButton_DragEnter;
                parent.StackRootPanel.Children.Add(btn);
            }
        }

        public static readonly DependencyProperty RatingValueHoverProperty = DependencyProperty.Register(
            "RatingValueHover",
            typeof(Int32),
            typeof(RatingBar),
            new PropertyMetadata(0, new PropertyChangedCallback(RatingValueHoverChanged)));

        public Int32 RatingValueHover
        {
            get
            {
                return (Int32)GetValue(RatingValueHoverProperty);
            }
            set
            {
                if (value < 0)
                {
                    SetValue(RatingValueHoverProperty, 0);
                }
                else if (value > NumberOfStars)
                {
                    SetValue(RatingValueHoverProperty, NumberOfStars);
                }
                else
                {
                    SetValue(RatingValueHoverProperty, value);
                }
            }
        }

        private static void RatingValueHoverChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RatingBar parent = sender as RatingBar;
            Int32 ratingValue = (Int32)e.NewValue;
            UIElementCollection children = (parent.Content as StackPanel).Children;

            ToggleButton button = null;
            for (Int32 i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = true;
            }

            for (Int32 i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = false;
            }

            System.Diagnostics.Debug.WriteLine($"Rating value: {ratingValue}");
        }

        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(SolidColorBrush),
            typeof(RatingBar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnStarForegroundColorChanged)));

        public SolidColorBrush StarForegroundColor
        {
            get { return (SolidColorBrush)GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        private static void OnStarForegroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            RatingBar control = (RatingBar)d;
            foreach (ToggleButton star in control.StackRootPanel.Children)
                star.Foreground = (SolidColorBrush)e.NewValue;

        }


        public static readonly DependencyProperty HeightValueProperty = DependencyProperty.Register(
            "HeightValue",
            typeof(Int32),
            typeof(RatingBar),
            new PropertyMetadata(30, new PropertyChangedCallback(HeightValueChanged)));

        public Int32 HeightValue
        {
            get
            {
                return (Int32)GetValue(HeightValueProperty);
            }
            set
            {
                SetValue(HeightValueProperty, value);
            }
        }

        private static void HeightValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RatingBar control = (RatingBar)sender;
            foreach (ToggleButton star in control.StackRootPanel.Children)
                star.Height = (int)e.NewValue;
        }

        public void RatingButtonClickEventHandler(Object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            RatingValue = (int)button.Tag;
            button.IsChecked = true;
        }

        public void ToggleButton_DragEnter(object sender, PointerRoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;
            RatingValueHover = (int)button.Tag;
        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RatingValueHover = RatingValue;
        }
    }

    public delegate void RatingValueChanged(object sender, RatingValueChangedArgs args);

    public class RatingValueChangedArgs : EventArgs
    {
        public int Value { get; }

        public RatingValueChangedArgs(int value)
        {
            Value = value;
        }
    }
}
