using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Messier16.Forms.Controls;
using Messier16.Forms.iOS.Controls;
using Messier16.Forms.iOS.Controls.Native.Checkbox;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]
namespace Messier16.Forms.iOS.Controls
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, M13Checkbox>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public new static void Init()
        {
            var temp = DateTime.Now;
        }



        private CGRect _originalBounds;

        /// <summary>
        /// Handles the Element Changed event
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            if (e.OldElement != null || Element == null) return;

            BackgroundColor = Element.BackgroundColor.ToUIColor();
            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property
                var width = (double)Constants.CheckboxDefaultHeight;
                if (Element.WidthRequest >= 0)
                {
                    width = Element.WidthRequest;
                }
                var checkBox = new M13Checkbox(new CGRect(0, 0, width, width))
                {
                    Bounds = new CGRect(0, 0, width, width)
                };
                SetNativeControl(checkBox);

                // Issue with list rendering
                _originalBounds = checkBox.Bounds;
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
                Control.SetCheckState(e.NewElement.Checked ? CheckboxState.Checked : CheckboxState.Unchecked);
                Control.SetEnabled(e.NewElement.IsEnabled);
                Control.Bounds = _originalBounds;
            }

            base.OnElementChanged(e);
        }

        private void Control_CheckedChanged(object sender, GenericEventArgs<bool> args)
        {
            Element.Checked = args.Value;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName == nameof(Element.IsEnabled))
            {
                Control.SetEnabled(Element.IsEnabled);
            }
            else if (e.PropertyName == nameof(Element.Checked))
            {
                Control.SetCheckState(Element.Checked ? CheckboxState.Checked : CheckboxState.Unchecked);
            }
        }
    }
}
