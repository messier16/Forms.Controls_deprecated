using System;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]
namespace Messier16.Forms.Android.Controls
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, CheckBox>, CompoundButton.IOnCheckedChangeListener
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            ((IViewController)Element).SetValueFromRenderer(Checkbox.CheckedProperty, isChecked);
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            SizeRequest sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

            if (sizeConstraint.Request.Width == 0)
            {
                int width = widthConstraint;
                if (widthConstraint <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Default values");
                    width = 100;
                }
                else if (widthConstraint <= 0)
                    width = 100;

                sizeConstraint = new SizeRequest(new Size(width, sizeConstraint.Request.Height), new Size(width, sizeConstraint.Minimum.Height));
            }

            return sizeConstraint;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                if (Element != null)
                    Element.CheckedChanged -= OnElementCheckedChanged;
                Control.SetOnCheckedChangeListener(null);
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
                e.OldElement.CheckedChanged -= OnElementCheckedChanged;

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var checkBox = new CheckBox(Context);
                    checkBox.SetOnCheckedChangeListener(this);
                    SetNativeControl(checkBox);
                }
                else
                {
                    UpdateEnabled();
                }

                e.NewElement.CheckedChanged += OnElementCheckedChanged;
                Control.Checked = e.NewElement.Checked;

            }

            base.OnElementChanged(e);
        }

        private void OnElementCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Control.Checked = Element.Checked;
        }

        private void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;
        }

        private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }
    }
}
