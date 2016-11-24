using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox
{
    public static class Constants
    {
        public const float BoxSize = 0.875f;
        public const float CheckHorizontalExtention = 0.125f;
        public const float CheckVerticalExtension = 0.125f;
        public const float CheckIndent = 0.125f;
        public const float CheckRaise = 0.1875f;
        public const float CheckSize = 0.8125f;
        public const float CheckBoxSpacing = 0.3125f;
        public const float M13CheckboxMaxFontSize = 100.0f;

        public const float CheckboxDefaultHeight = 25.0f;
        public const float BoxRadius = 0.1875f;
        public const float BoxStrokeWidth = 0.05f;
        public const float M13CheckboxHeightAutomatic = float.MaxValue;
    }

    public enum CheckboxAlignment
    {
        Right,
        Left,
        Center
    }

	public enum BoxType
	{
		/// The box is a circle.
		Circle,
		/// The box is square with optional rounded corners.
		Square
	}

	public enum MarkType
	{
		/// The mark is a standard checkmark.
		Checkmark,
		/// The mark is a radio style fill.
		Radio,
		/// The mark is an add/remove icon set.
		AddRemove,
		/// The mark is a disclosure indicator.
		Disclosure
	}

	public enum CheckState
	{
		/// No check is shown.
		Unchecked,
		/// A checkmark is shown.
		Checked,
		/// A dash is shown.
		Mixed
	}

		
}
