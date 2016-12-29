using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using RatingBar = Messier16.Forms.Controls.RatingBar;
using Messier16.Forms.Android.Controls.Native.RatingBar;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRenderer))]
namespace Messier16.Forms.Android.Controls
{
    public class RatingBarRenderer : ViewRenderer<RatingBar, Messier16RatingBar>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<RatingBar> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null) return;

            if (Control == null)
            {
                var layout = new LinearLayout(Context);

                // Instantiate the native control and assign it to the Control property
                var ratingBar = new Messier16RatingBar(Context)
                {
                    //IsIndicator = !Element.IsEnabled,
                    //StepSize = 1.0f,
                    //Max = Element.MaxRating,
                    MaxStars = Element.MaxRating,
                    FillColor = global::Android.Graphics.Color.Blue,
                    StrokeColor = global::Android.Graphics.Color.GreenYellow
                };

                // http://stackoverflow.com/questions/3858600/how-to-make-ratingbar-to-show-five-stars#comment4151898_3859248
                ratingBar.LayoutParameters = new LayoutParams(LayoutParams.WrapContent,LayoutParams.MatchParent);
                layout.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                //layout.AddView(ratingBar);
                SetNativeControl(ratingBar);
            }

            //if (e.OldElement != null)
            //{
            //    // Unsubscribe from event handlers and cleanup any resources
            //    var ratingBar = Control.GetChildAt(0) as NativeRatingBar;
            //    ratingBar.RatingBarChange -= Control_RatingBarChange;
            //}
            //if (e.NewElement != null)
            //{
            //    // Configure the control and subscribe to event handlers
            //    var ratingBar = Control.GetChildAt(0) as NativeRatingBar;
            //    ratingBar.RatingBarChange += Control_RatingBarChange;
            //}
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (Element == null || Control == null) return;

        //    if (e.PropertyName.Equals(nameof(RatingBar.Rating)))
        //    {
        //        var ratingBar = Control.GetChildAt(0) as NativeRatingBar;
        //        ratingBar.Rating = Element.Rating;
        //    }
        //    else
        //    {
        //        base.OnElementPropertyChanged(sender, e);
        //    }
        //}

        //private void Control_RatingBarChange(object sender, NativeRatingBar.RatingBarChangeEventArgs e)
        //{
        //    Element.Rating = (int)e.Rating;
        //}
    }
}