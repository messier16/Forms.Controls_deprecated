using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]
namespace Messier16.Forms.Android.Controls
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, CheckBox>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null) return;

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property
                var checkBox = new CheckBox(Context);
                if (Element.WidthRequest >= 0)
                {
                    checkBox.SetWidth((int)Element.WidthRequest);
                    checkBox.SetHeight((int)Element.WidthRequest);
                }
                SetNativeControl(checkBox);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                Control.CheckedChange -= CheckBoxCheckedChange;
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers

                // You should only create the control once (if Control is null), 
                // and then you should apply the values of NewElement to that control every time that NewElement is not null. Something like this:
                // from: https://forums.xamarin.com/discussion/comment/107477/#Comment_107477
                if (e.NewElement.WidthRequest >= 0)
                {
                    Control.SetHeight((int)e.NewElement.WidthRequest);
                    Control.SetWidth((int)e.NewElement.WidthRequest);
                }
                Control.Checked = e.NewElement.Checked;
                Control.Enabled = e.NewElement.IsEnabled;

                Control.CheckedChange += CheckBoxCheckedChange;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName.Equals(nameof(Checkbox.Checked)))
            {
                Control.Checked = Element.Checked;
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }

        private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }
    }
}
