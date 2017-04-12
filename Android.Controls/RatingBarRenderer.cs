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



            base.OnElementChanged(e);


            //if (e.OldElement != null)
            //    e.OldElement.Cha

            if (e.NewElement != null)
            {
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
                        FillColor = e.NewElement.FillColor.ToAndroid(),
                        StrokeColor = e.NewElement.FillColor.ToAndroid()
                    };

                    ratingBar.RatingChanged += RatingBarOnRatingChanged;

                    // http://stackoverflow.com/questions/3858600/how-to-make-ratingbar-to-show-five-stars#comment4151898_3859248
                    ratingBar.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.MatchParent);
                    layout.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                    //layout.AddView(ratingBar);
                    SetNativeControl(ratingBar);
                }
                //else
                //{
                //    UpdateEnabled();
                //}

                //e.NewElement.CheckedChanged += OnElementCheckedChanged;
                Control.Rating = e.NewElement.Rating;

            }

            
        }

        private void RatingBarOnRatingChanged(object sender, float f)
        {
            Element.Rating = (int)f;
        }
    }
}