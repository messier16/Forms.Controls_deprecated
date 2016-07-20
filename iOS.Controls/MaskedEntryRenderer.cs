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

		int DumbParse(string input)
		{
			var number = 0;

			int multiply = 1;
			for (int i = input.Length -1; i >= 0; i--)
			{
				if (Char.IsDigit(input[i]))
				{
					number += (input[i] - '0') * (multiply);
					multiply *= 10;
				}
			}
			return number;
		}

		bool shouldListen = true;

		// From http://stackoverflow.com/questions/34922331/getting-and-setting-cursor-position-of-uitextfield-and-uitextview-in-swift
		void FormatNumber(object sender, EventArgs e)
		{
			if (!shouldListen) return;
			var startPosition = Control.BeginningOfDocument;
			var selectedRange = Control.SelectedTextRange;

			shouldListen = false;
			var oldText = Control?.Text ?? "0";
			var number = DumbParse(oldText);

			var output = $"{number:#,###,###,##0}";

			Control.Text = output;

			var change = -1 *(oldText.Length - output.Length);
			var newPosition = Control.GetPosition(selectedRange.Start, (nint)change);

			if (newPosition != null) // before we fail miserably
			{
				Control.SelectedTextRange = Control.GetTextRange(newPosition, newPosition);
			}

			shouldListen = true;
		}
	}
}