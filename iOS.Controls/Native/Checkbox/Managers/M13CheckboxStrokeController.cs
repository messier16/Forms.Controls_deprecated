using System;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox.Managers
{
	public class M13CheckboxStrokeController : M13CheckboxController
	{
		#region Properties
		UIColor _tintColor
		public new UIColor TintColor
		{
			get { return _tintColor; }
			set
			{
				_tintColor = value;
				//	selectedBoxLayer.strokeColor = tintColor.cgColor
				//markLayer.strokeColor = tintColor.cgColor
			}
		}
		UIColor _secondaryTintColor
		public new UIColor SecondaryTintColor
		{
			get { return _secondaryTintColor; }
			set
			{
				_secondaryTintColor = value;
				//	unselectedBoxLayer.strokeColor = secondaryTintColor?.cgColor
			}
		}
		bool _hideBox;
		public bool HideBox
		{
			get { return _hideBox; }
			set
			{
				_hideBox = value;
			//	selectedBoxLayer.isHidden = hideBox
			//unselectedBoxLayer.isHidden = hideBox
			}
		}
		#endregion
	}
}
