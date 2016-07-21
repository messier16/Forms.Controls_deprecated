using System;
using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
	

	public class FormattedNumberEntry : Entry
	{

		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create(nameof(Value), typeof(int), typeof(FormattedNumberEntry), 0);

		public int Value
		{
			get { return (int)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public FormattedNumberEntry()
		{
			base.Keyboard = Keyboard.Numeric;
		}

		public new Keyboard Keyboard
		{
			get { return base.Keyboard; }
			set { if (value == Keyboard.Numeric) {} }
		}
	}
}

