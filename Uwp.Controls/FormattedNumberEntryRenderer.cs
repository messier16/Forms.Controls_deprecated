using Messier16.Forms.Controls;
using Messier16.Forms.Uwp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(FormattedNumberEntry), typeof(FormattedNumberEntryRenderer))]

namespace Messier16.Forms.Uwp.Controls
{
    public class FormattedNumberEntryRenderer : EntryRenderer
    {
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
                Control.TextChanged -= Control_TextChanged;
            }
            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                Control.TextChanged += Control_TextChanged;
            }
        }



        int DumbParse(string input)
        {
            var number = 0;

            int multiply = 1;
            for (int i = input.Length - 1; i >= 0; i--)
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

        private void Control_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {
            if (!shouldListen) return;
            shouldListen = false;

            var startPosition = Control.SelectionStart;

            var oldText = Control?.Text ?? "0";
            var number = DumbParse(oldText);

            var output = $"{number:#,###,###,##0}";

            var change = -1 * (oldText.Length - output.Length);
            
            Control.Text = output;
            Control.SelectionStart = startPosition + change;

           shouldListen = true;

        }
    }
}
