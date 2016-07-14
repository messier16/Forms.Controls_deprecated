using System;
using Foundation;
using Messier16.Forms.Controls;
using Messier16.Forms.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(MaskedEntry), typeof(MaskedEntryRenderer))]
namespace Messier16.Forms.iOS.Controls
{
	public class MaskedEntryRenderer : EntryRenderer
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public new static void Init()
		{
			var temp = DateTime.Now;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);


			if (Control == null)
			{
				return;
			}

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
				Control.EditingChanged -= FormatNumber;
			}
			if (e.NewElement != null)
			{
				// Configure the control and subscribe to event handlers
				Control.EditingChanged += FormatNumber;
			}
				
		}

		void FormatNumber(object sender, EventArgs e)
		{
			var text = Control?.Text;
			var number = 0.0;

			Double.TryParse(text, out number);

			var formatter = new NSNumberFormatter();
			formatter.NumberStyle = NSNumberFormatterStyle.Decimal;

			var output = formatter.StringFromNumber(number);
			Control.Text = output;
		}
	}
}