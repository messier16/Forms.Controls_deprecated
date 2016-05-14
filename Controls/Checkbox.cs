using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
    public class Checkbox : View
    {
        public static readonly BindableProperty CheckedProperty =
                BindableProperty.Create(nameof(Checked), typeof(bool), typeof(Checkbox), false, BindingMode.TwoWay);

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { if (Checked != value) SetValue(CheckedProperty, value); }
        }
    }
}
