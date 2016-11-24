using System;
using Messier16.Forms.iOS.Controls.Native.Checkbox;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox.Paths
{
	/// The base class that generates the paths for the different mark types.
	public  abstract class M13CheckboxPathGenerator
	{

		const double M_PI_2 = Math.PI / 2;

		//----------------------------
		// MARK: - Properties
		//----------------------------

		/// The maximum width or height the path will be generated with.
		public float Size = 0.0f;

		/// The line width of the created checkmark.
		public float CheckmarkLineWidth = 1.0f;

		/// The line width of the created box.
		public float BoxLineWidth = 1.0f;

		/// The corner radius of the box.
		public float CornerRadius = 3.0f;

		/// The box type to create.
		public BoxType BoxType = BoxType.Circle;

		//----------------------------
		// MARK: - Box Paths
		//----------------------------

		/**
		 Creates a path object for the box.
		 - returns: A `UIBezierPath` representing the box.
		 */
		public UIBezierPath PathForBox()
		{
			switch (BoxType)
			{
				case BoxType.Circle:
					return PathForCircle();
				case BoxType.Square:
					return PathForRoundedRect();

			}
			return null;
		}

		/**
		 Creates a circular path for the box. The path starts at the top center point of the box.
		 - returns: A `UIBezierPath` representing the box.
*/
		UIBezierPath PathForCircle()
		{

			var radius = (Size - BoxLineWidth) / 2.0;
			// Create a circle that starts in the top right hand corner.
			return UIBezierPath.FromOval(new CoreGraphics.CGRect(Size / 2.0, Size / 2.0, Size, Size));
			//     return new UIBezierPath(
			//	arcCenter: new CoreGraphics.CGPoint(),
			//radius: radius,
			//startAngle: M_PI_2,
			//endAngle: Math.PI * 2- M_PI_2,
			//clockwise: true)
		}

		/**
		 Creates a rounded rect path for the box. The path starts at the top center point of the box.
		 - returns: A `UIBezierPath` representing the box.
		 */
		UIBezierPath PathForRoundedRect()
		{
			var path = UIBezierPath.FromRoundedRect(new CoreGraphics.CGRect(Size / 2.0, Size / 2.0, Size, Size)
													, UIRectCorner.AllCorners, new CoreGraphics.CGSize(CornerRadius, CornerRadius));
			//let lineOffset: CGFloat = boxLineWidth / 2.0

			//      let trX: CGFloat = size - lineOffset - cornerRadius
			//let trY: CGFloat = 0.0 + lineOffset + cornerRadius
			//let tr = CGPoint(x: trX, y: trY)


			//let brX: CGFloat = size - lineOffset - cornerRadius
			//let brY: CGFloat = size - lineOffset - cornerRadius
			//let br = CGPoint(x: brX, y: brY)


			//let blX: CGFloat = 0.0 + lineOffset + cornerRadius
			//let blY: CGFloat = size - lineOffset - cornerRadius
			//let bl = CGPoint(x: blX, y: blY)


			//let tlX: CGFloat = 0.0 + lineOffset + cornerRadius
			//let tlY: CGFloat = 0.0 + lineOffset + cornerRadius
			//let tl = CGPoint(x: tlX, y: tlY)


			//path.move(to: CGPoint(x: (tl.x + tr.x) / 2.0, y: ((tl.y + tr.y) / 2.0) - cornerRadius))

			//      // Top side.
			//      let trYCr: CGFloat = tr.y - cornerRadius
			//path.addLine(to: CGPoint(x: tr.x, y: trYCr))

			//      // Right arc
			//      if cornerRadius != 0 {
			//          path.addArc(withCenter: tr,
			//                      radius: cornerRadius,
			//                      startAngle: CGFloat(-M_PI_2),
			//                      endAngle: 0.0,
			//                      clockwise: true)
			//      }
			//      // Right side.
			//      let brXCr: CGFloat = br.x + cornerRadius
			//path.addLine(to: CGPoint(x: brXCr, y: br.y))

			//      // Bottom right arc.
			//      if cornerRadius != 0 {
			//          path.addArc(withCenter: br,
			//                      radius: cornerRadius,
			//                      startAngle: 0.0,
			//                      endAngle: CGFloat(M_PI_2),
			//                      clockwise: true)
			//      }
			//      // Bottom side.
			//      let blYCr: CGFloat = bl.y + cornerRadius
			//path.addLine(to: CGPoint(x: bl.x , y: blYCr))
			//      // Bottom left arc.
			//      if cornerRadius != 0 {
			//          path.addArc(withCenter: bl,
			//                      radius: cornerRadius,
			//                      startAngle: CGFloat(M_PI_2),
			//                      endAngle: CGFloat(M_PI),
			//                      clockwise: true)
			//      }

			//      // Left side.
			//      let tlXCr: CGFloat = tl.x - cornerRadius
			//path.addLine(to: CGPoint(x: tlXCr, y: tl.y))
			//      // Top left arc.
			//      if cornerRadius != 0 {
			//          path.addArc(withCenter: tl,
			//                      radius: cornerRadius,
			//                      startAngle: CGFloat(M_PI),
			//                      endAngle: CGFloat(M_PI + M_PI_2),
			//                      clockwise: true)
			//      }
			//      path.close()
			return path;
		}

		/**
		 Creates a small dot for the box.
		 - returns: A `UIBezierPath` representing the dot.
		 */
		UIBezierPath PathForDot()
		{
			var boxPath = PathForBox();
			var scale = 1.0f / 20.0f;
			boxPath.ApplyTransform(CoreGraphics.CGAffineTransform.Scale(new CoreGraphics.CGAffineTransform(), scale, scale));
			//boxPath?.apply(CGAffineTransform(scaleX: scale, y: scale))
			//boxPath?.apply(CGAffineTransform(translationX: (size - (size* scale)) / 2.0, y: (size - (size* scale)) / 2.0))
			return boxPath;
		}

		//----------------------------
		// MARK: - Check Generation
		//----------------------------

		/**
		 Generates the path for the mark for the given state.
		 - parameter state: The state to generate the mark path for.
		 - returns: A `UIBezierPath` representing the mark.
		*/
		public UIBezierPath PathForMark(CheckState state)
		{

			switch (state)
			{
				case CheckState.Unchecked:
					return PathForUnselectedMark();
				case CheckState.Checked:
					return PathForMark();
				case CheckState.Mixed:
					return PathForMixedMark();
			}
			return null;
		}

		/**
		 Generates the path for the long mark for the given state used in the spiral animation.
		 - parameter state: The state to generate the long mark path for.
		 - returns: A `UIBezierPath` representing the long mark.
		*/
		public UIBezierPath PathForLongMark(CheckState state)
		{


			switch (state)
			{
				case CheckState.Unchecked:
					return PathForLongUnselectedMark();
				case CheckState.Checked:
					return PathForLongMark();
				case CheckState.Mixed:
					return PathForLongMixedMark();
			}
			return null;
		}

		/**
		 Creates a path object for the mark.
		 - returns: A `UIBezierPath` representing the mark.
		 */
		public abstract UIBezierPath PathForMark();

		/**
		 Creates a path object for the long mark.
		 - returns: A `UIBezierPath` representing the long mark.
		 */
		public abstract UIBezierPath PathForLongMark();

		/**
		 Creates a path object for the mixed mark.
		 - returns: A `UIBezierPath` representing the mixed mark.
		 */
		public abstract UIBezierPath PathForMixedMark();

		/**
		 Creates a path object for the long mixed mark.
		 - returns: A `UIBezierPath` representing the long mixed mark.
		 */
		public abstract UIBezierPath PathForLongMixedMark();

		/**
		 Creates a path object for the unselected mark.
		 - returns: A `UIBezierPath` representing the unselected mark.
		 */
		public abstract UIBezierPath PathForUnselectedMark();

		/**
		 Creates a path object for the long unselected mark.
		 - returns: A `UIBezierPath` representing the long unselected mark.
		 */
		public abstract UIBezierPath PathForLongUnselectedMark();

	}
}

