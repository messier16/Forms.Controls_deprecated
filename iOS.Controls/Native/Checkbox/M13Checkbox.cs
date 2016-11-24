using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox
{

    [Register("M13Checkbox")]
    public class M13Checkbox : UIControl
    {
        public M13Checkbox()
            : this(new CGRect(0, 0, Constants.CheckboxDefaultHeight, Constants.CheckboxDefaultHeight))
        {
        }

        public M13Checkbox(CGRect frame)
            : base(frame)
        {
            //Setup();
        }

        public M13Checkbox(NSCoder coder)
            : base(coder)
        {
            //Setup();
        }

    }
}
