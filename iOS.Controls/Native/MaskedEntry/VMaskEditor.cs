using System;
using Foundation;
using UIKit;

namespace Messier16.Forms.iOS.Controls
{
	public class VMaskEditor : NSObject
	{
		public bool ShouldChangeCharactersInRange(NSRange range,
												 NSString str,
												 UITextField textField,
												 NSString mask)
		{
			//StringByReplacingCharactersInRange
			//var currentTextDigited = textField.Text.StringByReplacingCharactersInRange
			var currentTextDigited = textField.Text.Substring(0, (int)range.Location)
											  + str
			                                  + textField.Text.Substring((int)range.Location, (int)range.Length);

			if (str.Length == 0)
			{
				char lastCharDeleted = '\0';
				while (currentTextDigited.Length > 0 && 
				       !IsNumber(currentTextDigited[currentTextDigited.Length -1]))
				{
					lastCharDeleted = currentTextDigited[currentTextDigited.Length - 1];
					currentTextDigited = currentTextDigited.Substring(0, currentTextDigited.Length - 1);
				}
				textField.Text = currentTextDigited;
				return false;
			}

			NSMutableString returnText = new NSMutableString();
			if (currentTextDigited.Length > mask.Length)
			{
				return false;
			}

			int last = 0;
			bool needAppend = false;

			for (int i = 0; i < currentTextDigited.Length; i++)
			{
				var currentCharMask = mask[i];
				var currentChar = currentTextDigited[i];
				if (IsNumber(currentChar) && currentCharMask == '#')
				{
					returnText.Append(new NSString($"{currentChar}"));
				}
				else 
				{
					if (currentCharMask == '#')
					{
						break;
					}
					if (IsNumber(currentChar) && currentCharMask != currentChar)
					{
						needAppend = true;
					}
					returnText.Append(new NSString($"{currentChar}"));
				}
				last = i;
			}

			// https://github.com/viniciusmo/VMaskTextField/blob/master/Pod/Classes/VMaskEditor.mif
			//for (int i = last + 1; i < mask.length; i++)
			//{
			//	unichar currentCharMask = [mask characterAtIndex: i];
			//	if (currentCharMask != '#')
			//	{

			//[returnText appendString:[NSString stringWithFormat:@"%c", currentCharMask]];
			//     }
			//     if (currentCharMask == '#') {
			//         break;
			//     }
			// }
			// if (needAppend) {
			//     [returnText appendString:string];
			// }
			// textField.text = returnText;

			return false;
		}

		bool IsNumber(char c)
		{
			return 0 <= (c - '0') && (c - '0') <= 9;
		}
	}
}

