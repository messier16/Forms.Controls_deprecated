using System;
using UIKit;
using CoreGraphics;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox.Paths
{
	public class M13CheckboxCheckPathGenerator : M13CheckboxPathGenerator
	{

		//----------------------------
		// MARK: - Structures
		//----------------------------

		/// Contains the geometry information needed to generate the checkmark, as well as generates the locations of the feature points.
		public class CheckmarkProperties
		{

			/// The angle between the x-axis, and the line created between the origin, and the location where the extended long arm of the checkmark meets the box. (Diagram: Θ)
			internal float longArmBoxIntersectionAngle = 45.0f * ((float)Math.PI / 180.0f);


			public class Radiuses
			{
				public Radiuses(float circle, float box)
				{
					Circle = circle;
					Box = box;
				}
				public float Circle { get; set; }
				public float Box { get; set; }
			}
			///// The distance from the center the long arm of the checkmark draws to, as a percentage of size. (Diagram: S)
			public Radiuses longArmRadius = new Radiuses(circle: 0.22f, box: 0.33f);

			/// The distance from the center of the middle/bottom point of the checkbox, as a percentage of size. (Diagram: T)
			public Radiuses middlePointRadius = new Radiuses(circle: 0.133f, box: 0.1995f);

			/// The distance between the horizontal center and the middle point of the checkbox.
			public Radiuses shortArmRadiusmiddlePointOffset = new Radiuses(circle: -0.04f, box: -0.06f);

			/// The distance from the center of the left most point of the checkmark, as a percentage of size.
			public Radiuses shortArmRadius = new Radiuses(circle: 0.17f, box: 0.255f);

			/// The distance between the vertical center and the left most point of the checkmark, as a percentage of size.
			public Radiuses shortArmOffset = new Radiuses(circle: 0.02f, box: 0.03f);
		}

		//----------------------------
		// MARK: - Properties
		//----------------------------

		/// The parameters that define the checkmark.
		CheckmarkProperties checkmarkProperties = new CheckmarkProperties();

		//----------------------------
		// MARK: - Points of Intrest
		//----------------------------

		/// The intersection point between the extended long checkmark arm, and the box.
		public CGPoint checkmarkLongArmBoxIntersectionPoint
		{
			get
			{

				var cornerRadius = this.CornerRadius;
				var boxLineWidth = this.BoxLineWidth;
				var size = this.Size;

				var radius = (size - boxLineWidth) / 2.0;

				var theta = checkmarkProperties.longArmBoxIntersectionAngle;

				if (this.BoxType == BoxType.Circle)
				{
					// Basic trig to get the location of the point on the circle.
					var x = (size / 2.0f) + (radius * Math.Cos(theta));
					var y = (size / 2.0) - (radius * Math.Sin(theta));
					return new CGPoint(x: x, y: y);

				}
				else {
					// We need to differentiate between the box edges and the rounded corner.
					var lineOffset = boxLineWidth / 2.0f;
					var circleX = size - lineOffset - cornerRadius;
					var circleY = 0.0 + lineOffset + cornerRadius;
					var edgeX = (size / 2.0) + (0.5 * (size - boxLineWidth) * (1.0 / Math.Tan(theta)));
					var edgeY = (size / 2.0) - (0.5 * (size - boxLineWidth) * Math.Tan(theta));

					if (edgeX <= circleX)
					{
						// On the top edge.
						return new CGPoint(x: edgeX, y: lineOffset);

					}
					else if (edgeY >= circleY)
					{
						// On the right edge.
						var x = size - lineOffset;
						return new CGPoint(x: x, y: edgeY);
					}
					else {
						// On the corner
						var cos2Theta = Math.Cos(2.0 * theta);

						var sin2Theta = Math.Sin(2.0 * theta);

						var powC = Math.Pow((-2.0 * cornerRadius) + size, 2.0);

						var a = size * (3.0 + cos2Theta + sin2Theta);
						var b = -2.0 * cornerRadius * (Math.Cos(theta) + Math.Sin(theta));
						var c = (((4.0 * cornerRadius) - size) * size) + (powC * sin2Theta);
						var d = size * Math.Cos(theta) * (Math.Cos(theta) - Math.Sin(theta));
						var e = 2.0 * cornerRadius * Math.Sin(theta) * (Math.Cos(theta) + Math.Sin(theta));

						var x = 0.25 * (a + (2.0 * (b + Math.Sqrt(c)) * Math.Cos(theta))) - boxLineWidth;
						var y = 0.50 * (d + e - (Math.Sqrt(c) * Math.Sin(theta))) + boxLineWidth;


						return new CGPoint(x: x, y: y);
					}
				}
			}
		}

		CGPoint checkmarkLongArmEndPoint
		{
			get
			{
				var size = this.Size;
				var boxLineWidth = this.BoxLineWidth;

				// Known variables
				var boxEndPoint = checkmarkLongArmBoxIntersectionPoint;
				var x2 = boxEndPoint.X;
				var y2 = boxEndPoint.Y;
				var midPoint = checkmarkMiddlePoint;
				var x1 = midPoint.X;
				var y1 = midPoint.Y;
				var r = this.BoxType == BoxType.Circle ? size * checkmarkProperties.longArmRadius.Circle : size * checkmarkProperties.longArmRadius.Box;

				var a1 = (size * Math.Pow(x1, 2.0)) - (2.0 * size * x1 * x2) + (size * Math.Pow(x2, 2.0)) + (size * x1 * y1) - (size * x2 * y1);
				var a2 = (2.0 * x2 * Math.Pow(y1, 2.0)) - (size * x1 * y2) + (size * x2 * y2) - (2.0 * x1 * y1 * y2) - (2.0 * x2 * y1 * y2) + (2.0 * x1 * Math.Pow(y2, 2.0));
				var b = -16.0 * (Math.Pow(x1, 2.0) - (2.0 * x1 * x2) + Math.Pow(x2, 2.0) + Math.Pow(y1, 2.0) - (2.0 * y1 * y2) + Math.Pow(y2, 2.0));
				var c1 = Math.Pow(r, 2.0) * ((-1 * Math.Pow(x1, 2.0)) + (2.0 * x1 * x2) - Math.Pow(x2, 2.0));
				var c2 = Math.Pow(size, 2.0) * ((0.5 * Math.Pow(x1, 2.0)) - (x1 * x2) + (0.5 * Math.Pow(x2, 2.0)));
				var d1 = (Math.Pow(x2, 2.0) * Math.Pow(y1, 2.0)) - (2.0 * x1 * x2 * y1 * y2) + (Math.Pow(x1, 2.0) * Math.Pow(y2, 2.0));
				var d2 = size * ((x1 * x2 * y1) - (Math.Pow(x2, 2.0) * y1) - (Math.Pow(x1, 2.0) * y2) + (x1 * x2 * y2));
				var cd = c1 + c2 + d1 + d2;
				var e1 = (x1 * ((4.0 * y1) - (4.0 * y2)) * y2) + (x2 * y1 * ((-4.0 * y1) + (4.0 * y2)));
				var e2 = size * ((-2.0 * Math.Pow(x1, 2.0)) + (x2 * ((-2.0 * x2) + (2.0 * y1) - (2.0 * y2))) + (x1 * (4.0 * x2 - (2.0 * y1) + (2.0 * y2))));
				var f = Math.Pow(x1, 2.0) - (2.0 * x1 * x2) + Math.Pow(x2, 2.0) + Math.Pow(y1, 2.0) - (2.0 * y1 * y2) + Math.Pow(y2, 2);
				var g1 = (0.5 * size * x1 * y1) - (0.5 * size * x2 * y1) - (x1 * x2 * y1) + (Math.Pow(x2, 2.0) * y1) + (0.5 * size * Math.Pow(y1, 2.0));
				var g2 = (-0.5 * size * x1 * y2) + (Math.Pow(x1, 2.0) * y2) + (0.5 * size * x2 * y2) - (x1 * x2 * y2) - (size * y1 * y2) + (0.5 * size * Math.Pow(y2, 2.0));


				var h1 = (-4.0 * Math.Pow(x2, 2.0) * y1) - (4.0 * Math.Pow(x1, 2.0) * y2) + (x1 * x2 * ((4.0 * y1) + (4.0 * y2)));

				var h2 = size * ((-2.0 * x1 * y1) + (2.0 * x2 * y1) - (2.0 * Math.Pow(y1, 2.0)) + (2.0 * x1 * y2) - (2.0 * x2 * y2) + (4.0 * y1 * y2) - (2.0 * Math.Pow(y2, 2.0)));


				var i = (Math.Pow(r, 2.0) * (-Math.Pow(y1, 2.0) + (2.0 * y1 * y2) - Math.Pow(y2, 2.0))) +
					(Math.Pow(size, 2.0) * ((0.5 * Math.Pow(y1, 2.0)) - (y1 * y2) + (0.5 * Math.Pow(y2, 2.0))));

				var j = size * ((x1 * (y1 - y2) * y2) + (x2 * y1 * (-y1 + y2)));
				var powE1E2 = Math.Pow(e1 + e2, 2.0);
				var subX1 = (b * cd) + powE1E2;

				var subX2 = (a1 + a2 + (0.5 * Math.Sqrt(subX1)));


				var powH1H2 = Math.Pow(h1 + h2, 2.0);
				var subY1 = powH1H2 + (b * (d1 + i + j));
				var subY2 = (0.25 * Math.Sqrt(subY1));
				var x = (0.5 * subX2 + (boxLineWidth / 2.0)) / f;
				var y = (g1 + g2 - subY2 + (boxLineWidth / 2.0)) / f;


				return new CGPoint(x: x, y: y);
			}
		}

		CGPoint checkmarkMiddlePoint
		{
			get
			{
				var r = this.BoxType == BoxType.Circle ? checkmarkProperties.middlePointRadius.Circle : checkmarkProperties.middlePointRadius.Box;
				var o = this.BoxType == BoxType.Circle ? checkmarkProperties.middlePointOffset.Circle : checkmarkProperties.middlePointOffset.Box;


				var size = this.Size;
				var x = (size / 2.0) + (size * o);
				var y = (size / 2.0) + (size * r);

				return new CGPoint(x: x, y: y);
			}

		}

		CGPoint checkmarkShortArmEndPoint
		{
			get
			{

				var r = this.BoxType == BoxType.Circle ? checkmarkProperties.shortArmRadius.Circle : checkmarkProperties.shortArmRadius.Box;
				var o = this.BoxType == BoxType.Circle ? checkmarkProperties.shortArmRadius.Circle : checkmarkProperties.shortArmRadius.Box;
				var size = this.Size;
				var x = (size / 2.0) - (size * r);
				var y = (size / 2.0) + (size * o);
				return new CGPoint(x: x, y: y);
			}
		}

		//----------------------------
		// MARK: - Box Paths
		//----------------------------

		//public override UIBezierPath PathForCircle() {
		//    //var radius = (size - boxLineWidth) / 2.0
		//    //// Create a circle that starts in the top right hand corner.
		//    //return UIBezierPath(arcCenter: CGPoint(x: size / 2.0, y: size / 2.0),
		//    //                    radius: radius,
		//    //                    startAngle: -checkmarkProperties.longArmBoxIntersectionAngle,
		//    //                    endAngle(2 * M_PI) - checkmarkProperties.longArmBoxIntersectionAngle,
		//    //                    clockwise: true)
		//}

		//public override UIBezierPath PathForRoundedRect() {
		//    var path = UIBezierPath()

		//		var lineOffset = boxLineWidth / 2.0

		//        var trX = size - lineOffset - cornerRadius
		//		var trY = 0.0 + lineOffset + cornerRadius
		//		var tr = CGPoint(x: trX, y: trY)


		//		var brX = size - lineOffset - cornerRadius
		//		var brY = size - lineOffset - cornerRadius
		//		var br = CGPoint(x: brX, y: brY)


		//		var blX = 0.0 + lineOffset + cornerRadius
		//		var blY = size - lineOffset - cornerRadius
		//		var bl = CGPoint(x: blX, y: blY)


		//		var tlX = 0.0 + lineOffset + cornerRadius
		//		var tlY = 0.0 + lineOffset + cornerRadius
		//		var tl = CGPoint(x: tlX, y: tlY)

		//		// Start in the top right corner.
		//var offset = ((cornerRadius* sqrt(2)) / 2.0)
		//        var trXOffset = tr.x + offset
		//		var trYOffset = tr.y - offset
		//		path.move(to: CGPoint(x: trXOffset, y: trYOffset))
		//        // Bottom of top right arc.12124
		//        if cornerRadius != 0 {
		//            path.addArc(withCenter: tr,
		//                        radius: cornerRadius,
		//                        startAngle(-M_PI_4),
		//                        endAngle: 0.0,
		//                        clockwise: true)
		//        }
		//        // Right side.
		//        var brXCr = br.x + cornerRadius
		//		path.addLine(to: CGPoint(x: brXCr, y: br.y))

		//        // Bottom right arc.
		//        if cornerRadius != 0 {
		//            path.addArc(withCenter: br,
		//                        radius: cornerRadius,
		//                        startAngle: 0.0,
		//                        endAngle(M_PI_2),
		//                        clockwise: true)
		//        }
		//        // Bottom side.
		//        var blYCr = bl.y + cornerRadius
		//		path.addLine(to: CGPoint(x: bl.x , y: blYCr))
		//        // Bottom left arc.
		//        if cornerRadius != 0 {
		//            path.addArc(withCenter: bl,
		//                        radius: cornerRadius,
		//                        startAngle(M_PI_2),
		//                        endAngle(M_PI),
		//                        clockwise: true)
		//        }
		//        // Left side.
		//        var tlXCr = tl.x - cornerRadius
		//		path.addLine(to: CGPoint(x: tlXCr, y: tl.y))
		//        // Top left arc.
		//        if cornerRadius != 0 {
		//            path.addArc(withCenter: tl,
		//                        radius: cornerRadius,
		//                        startAngle(M_PI),
		//                        endAngle(M_PI + M_PI_2),
		//                        clockwise: true)
		//        }
		//        // Top side.
		//        var trYCr = tr.y - cornerRadius
		//		path.addLine(to: CGPoint(x: tr.x, y: trYCr))
		//        // Top of top right arc
		//        if cornerRadius != 0 {
		//            path.addArc(withCenter: tr,
		//                        radius: cornerRadius,
		//                        startAngle(M_PI + M_PI_2),
		//                        endAngle(M_PI + M_PI_2 + M_PI_4),
		//                        clockwise: true)
		//        }
		//        path.close()
		//return path;
		// }

		//----------------------------
		// MARK: - Mark Generation
		//----------------------------

		public override UIBezierPath PathForMark()
		{
			var path = new UIBezierPath();


			//path.move(to: checkmarkShortArmEndPoint)
			//      path.addLine(to: checkmarkMiddlePoint)
			//      path.addLine(to: checkmarkLongArmEndPoint)

			return path;
		}

		public override UIBezierPath PathForLongMark()
		{

			var path = new UIBezierPath();

			//path.move(to: checkmarkShortArmEndPoint)
			//      path.addLine(to: checkmarkMiddlePoint)
			//      path.addLine(to: checkmarkLongArmBoxIntersectionPoint)

			return path;
		}

		public override UIBezierPath PathForMixedMark()
		{
			var path = new UIBezierPath();


			//path.move(to: CGPoint(x: size* 0.25, y: size* 0.5))
			//      path.addLine(to: CGPoint(x: size* 0.5, y: size* 0.5))
			//      path.addLine(to: CGPoint(x: size* 0.75, y: size* 0.5))

			return path;
		}

		public override UIBezierPath PathForLongMixedMark()
		{
			var path = new UIBezierPath();


			//path.move(to: CGPoint(x: size* 0.25, y: size* 0.5))
			//      path.addLine(to: CGPoint(x: size* 0.5, y: size* 0.5))
			//      path.addLine(to: CGPoint(x: size - boxLineWidth, y: size* 0.5))

			return path;
		}

		public override UIBezierPath PathForUnselectedMark()
		{
			return null;
		}

		public override UIBezierPath PathForLongUnselectedMark()
		{
			return null;
		}
	}
}
