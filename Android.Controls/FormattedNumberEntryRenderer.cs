using System;
using Messier16.Forms.Controls;
using Messier16.Forms.Android.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using Java.Text;

[assembly: ExportRenderer(typeof(FormattedNumberEntry), typeof(FormattedNumberEntryRenderer))]
namespace Messier16.Forms.Android.Controls
{
	public class FormattedNumberEntryRenderer : EntryRenderer
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
				Control.AfterTextChanged -= Control_AfterTextChanged; 
			}
			if (e.NewElement != null)
			{
				// Configure the control and subscribe to event handlers
				Control.AfterTextChanged += Control_AfterTextChanged;
			}
				
		}

		bool shouldListen = true;

		void Control_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
		{
			if (!shouldListen) return;
			var cursorPosition = Control.SelectionStart;

			shouldListen = false;
			var oldText = Control?.Text ?? "0";
			DecimalFormat df2 = new DecimalFormat("#,###,###,##0.00");
			var number = 0m;
			Decimal.TryParse(oldText, out number);


			var output = $"{number:#,###,###,##0.00}";

			Control.Text = output;

			var change = oldText.Length - output.Length;
			Control.SetSelection(cursorPosition - change);

			shouldListen =true;
		}

		void Control_TextChanged(object sender,  global::Android.Text.TextChangedEventArgs e)
		{

		}
	}
}