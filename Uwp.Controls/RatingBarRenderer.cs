using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Messier16.Forms.Controls;
using Messier16.Forms.Uwp.Controls;
using Xamarin.Forms.Platform.UWP;
using NativeRatingBar = Messier16.Forms.Uwp.Controls.Native.RatingBar.RatingBar;


[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRenderer))]
namespace Messier16.Forms.Uwp.Controls
{
    public class RatingBarRenderer : ViewRenderer<RatingBar, NativeRatingBar>
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

                // Instantiate the native control and assign it to the Control property
                var ratingBar = new NativeRatingBar()
                {
                    NumberOfStars = Element.MaxRating,
                    RatingValue = Element.Rating,
                    StarForegroundColor = new SolidColorBrush(Color.FromArgb(255, 10, 10, 10)),
                    HeightValue = 30
                };
                SetNativeControl(ratingBar);
            }

            if (e.OldElement != null)
            {
                Control.RatingValueChanged -= Control_RatingValueChanged;
            }
            if (e.NewElement != null)
            {
                Control.RatingValueChanged += Control_RatingValueChanged;
            }
        }

        private void Control_RatingValueChanged(object sender, Native.RatingBar.RatingValueChangedArgs args)
        {
            Element.Rating = args.Value;
        }
    }
}
