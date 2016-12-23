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

            base.OnElementChanged(e);


            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property
                var width = FormsCheckbox.DefaultSize;
                if (Element.WidthRequest > 0)
                {
                    width = Element.WidthRequest;
                }

                System.Diagnostics.Debug.WriteLine($"{Element.AutomationId}: {width}");
                var checkBox = new Checkbox(new CGRect(0, 0, width, width))
                {
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
                var element = e.NewElement;

                // Configure the control and subscribe to event handlers
                Control.CheckedChanged += Control_CheckedChanged;

                Control.Enabled = element.IsEnabled;
                //Control.Hidden = !element.IsVisible;
                Control.Checked = element.Checked;
                if (element.CheckboxBackgroundColor != Color.Default)
                    Control.CheckboxBackgroundColor = element.CheckboxBackgroundColor.ToUIColor();

                if (element.TickColor != Color.Default)
                    Control.TickColor = element.TickColor.ToUIColor();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("NewElement is null");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
                Control.CheckedChanged -= Control_CheckedChanged;
            base.Dispose(disposing);

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
            else if (e.PropertyName == nameof(Element.CheckboxBackgroundColor))
            {
                Control.CheckboxBackgroundColor = Element.CheckboxBackgroundColor.ToUIColor();
            }
            //else if (e.PropertyName == nameof(Element.IsVisible))
            //{
            //    Control.Hidden = !Element.IsVisible;
            //}
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }
    }
}
