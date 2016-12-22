using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Messier16.Forms.iOS.Controls.Native.Checkbox;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsCheckbox = Messier16.Forms.Controls.Checkbox;
using Messier16.Forms.iOS.Controls;

[assembly: ExportRenderer(typeof(FormsCheckbox), typeof(CheckboxRenderer))]
namespace Messier16.Forms.iOS.Controls
{
    public class CheckboxRenderer : ViewRenderer<FormsCheckbox, Checkbox>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public new static void Init()
        {
            var temp = DateTime.Now;
        }

        /// <summary>
        /// Handles the Element Changed event
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<FormsCheckbox> e)
        {
            if (e.OldElement != null || Element == null) return;

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property
                var width = 40d;
                if (Element.WidthRequest >= 0)
                {
                    width = Element.WidthRequest;
                }
                var checkBox = new Native.Checkbox.Checkbox(new CGRect(0, 0, width, width))
                {
                    //Bounds = new CGRect(0, 0, width, width)
                };
                SetNativeControl(checkBox);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                Control.CheckedChanged -= Control_CheckedChanged;
            }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                Control.CheckedChanged += Control_CheckedChanged;

                Control.Enabled = e.NewElement.IsEnabled;
                Control.Checked = e.NewElement.Checked;
                if (e.NewElement.CheckboxBackgroundColor != Color.Default)
                    Control.CheckboxBackgroundColor = e.NewElement.CheckboxBackgroundColor.ToUIColor();

                if (e.NewElement.TickColor != Color.Default)
                    Control.TickColor = e.NewElement.TickColor.ToUIColor();
            }

            base.OnElementChanged(e);
        }




        private void Control_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Element.Checked = e.Checked;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName == nameof(Element.IsEnabled))
            {
                Control.Enabled = Element.IsEnabled;
            }
            else if (e.PropertyName == nameof(Element.Checked))
            {
                Control.Checked = Element.Checked;
            }
            else if (e.PropertyName == nameof(Element.TickColor))
            {
                Control.TickColor = Element.TickColor.ToUIColor();
            }
            else if (e.PropertyName == nameof(Element.BackgroundColor))
            {
                Control.CheckboxBackgroundColor = Element.BackgroundColor.ToUIColor();
            }
            else if (e.PropertyName == nameof(Element.IsVisible))
            {
                Control.Hidden = !Element.IsVisible;
                Control.SetNeedsDisplay();
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
            //else if (e.PropertyName == nameof(Element.IsVisible))
            //{
            //	Control.Hidden = !Element.IsVisible;
            //}
        }
    }
}
