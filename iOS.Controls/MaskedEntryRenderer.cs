using System;
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
		
	}
}

