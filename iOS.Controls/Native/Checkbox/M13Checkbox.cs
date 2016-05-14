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
            Setup();
        }

        public M13Checkbox(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        //        - (id)initWithTitle:(NSString *)title
        //        {
        //            self = [self initWithFrame:CGRectMake(0, 0, 100.0, M13CheckboxDefaultHeight) title:title checkHeight:M13CheckboxDefaultHeight];
        //            if (self) {
        //                CGSize labelSize;
        //                if ([title respondsToSelector:@selector(sizeWithAttributes:)]) {
        //                    labelSize = [title sizeWithAttributes:@{ NSFontAttributeName: _titleLabel.font }];
        //                } else {
        //
        //                    NSDictionary *attributes = @{NSFontAttributeName: _titleLabel.font};
        //                    labelSize = [_titleLabel.text sizeWithAttributes:attributes];
        //                    labelSize =CGSizeMake(ceilf(labelSize.width), ceilf(labelSize.height));
        //
        //                }
        //
        //                self.frame = CGRectMake(
        //                    self.frame.origin.x,
        //                    self.frame.origin.y,
        //                    labelSize.width + ([self heightForCheckbox] * kCheckBoxSpacing) + ((kBoxSize + kCheckHorizontalExtention) * [self heightForCheckbox]),
        //                    self.frame.size.height);
        //            }
        //            return self;
        //        }

        //        - (id)initWithFrame:(CGRect)frame title:(NSString *)title
        //        {
        //            self = [self initWithFrame:frame title:title checkHeight:M13CheckboxDefaultHeight];
        //            if (self) {
        //
        //            }
        //            return self;
        //        }

        //        - (id)initWithFrame:(CGRect)frame title:(NSString *)title checkHeight:(CGFloat)checkHeight
        //        {
        //            self = [super initWithFrame:frame];
        //            if (self) {
        //                //Set the title label text
        //                _checkHeight = checkHeight;
        //                [self setup];
        //                _titleLabel.text = title;
        //                [self layoutSubviews];
        //            }
        //            return self;
        //        }

        void Setup()
        {
            // Set the basic properties
            var heightForCheckbox = HeightForCheckbox();
            //Flat = true;
            StrokeColor = UIColor.FromRGBA((System.nfloat) 0.02, (System.nfloat) 0.47, (System.nfloat) 1.0,
                (System.nfloat) 1.0);
            StrokeWidth = Constants.BoxStrokeWidth*heightForCheckbox;
            CheckColor = UIColor.FromRGBA((System.nfloat) 0.02, (System.nfloat) 0.47, (System.nfloat) 1.0,
                (System.nfloat) 1.0);
            TintColor = UIColor.FromRGBA((System.nfloat) 1.0, (System.nfloat) 1.0, (System.nfloat) 1.0,
                (System.nfloat) 1.0);
            UncheckedColor = UIColor.FromRGBA(0, 0, 0, 0);
            ClipsToBounds = false;
            BackgroundColor = UIColor.Clear;
            Radius = Constants.BoxRadius*heightForCheckbox;
            CheckAlignment = CheckboxAlignment.Right;
            CheckState = CheckboxState.Unchecked;
            Enabled = true;

            // Create the check view
            var checkViewFrame = new CGRect(
                Frame.Size.Width - ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox),
                (Frame.Size.Height - ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox))/
                (System.nfloat) 2.0,
                ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox),
                heightForCheckbox)
                .Integral();

            CheckView = new CheckView(checkViewFrame);
            CheckView.Checkbox = this;
            CheckView.Selected = false;
            CheckView.BackgroundColor = UIColor.Clear;
            CheckView.ClipsToBounds = false;
            CheckView.UserInteractionEnabled = false;

            // Create the title label
            CGRect labelFrame = new CGRect(
                0,
                0,
                Frame.Size.Width - CheckView.Frame.Size.Width - (heightForCheckbox*Constants.CheckBoxSpacing),
                Frame.Size.Height)
                .Integral();

            TitleLabel = new UILabel(labelFrame);
            TitleLabel.Text = "";
            TitleLabel.TextColor = UIColor.FromRGBA(0, 0, 0, 1);
            TitleLabel.Font = UIFont.BoldSystemFontOfSize(16);
            TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            TitleLabel.BackgroundColor = UIColor.Clear;
            TitleLabel.UserInteractionEnabled = false;

            AddGestureRecognizer(new UITapGestureRecognizer(ToggleCheckState));

            AutoFitFontToHeight();
            AddSubview(CheckView);
            AddSubview(TitleLabel);
            LayoutSubviews();
        }

        nfloat HeightForCheckbox()
        {
            //See if the checkbox's height is automatic, and return the proper size
            //            return System.Math.Abs(_checkHeight - M13CheckboxHeightAutomatic) < 0.2 ? Frame.Size.Height : _checkHeight;
            return Bounds.Size.Width;
        }

        private UIBezierPath _defaultShape;

        public UIBezierPath DefaultShape
        {
            get
            {
                if (_defaultShape == null)
                {
                    var height = HeightForCheckbox();
                    _defaultShape = new UIBezierPath();
                    _defaultShape.MoveTo(new CGPoint((0.17625*height), (0.368125*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.17625*height), (0.46375*height)),
                        new CGPoint((0.13125*height), (0.418125*height)),
                        new CGPoint((0.17625*height), (0.46375*height)));
                    _defaultShape.AddLineTo(new CGPoint((0.4*height), (0.719375*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.45375*height), (0.756875*height)),
                        new CGPoint((0.4*height), (0.719375*height)),
                        new CGPoint((0.4275*height), (0.756875*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.505625*height), (0.719375*height)),
                        new CGPoint((0.480625*height), (0.75625*height)),
                        new CGPoint((0.505625*height), (0.719375*height)));
                    _defaultShape.AddLineTo(new CGPoint((0.978125*height), (0.145625*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.978125*height), (0.050625*height)),
                        new CGPoint((0.978125*height), (0.145625*height)),
                        new CGPoint((1.026875*height), (0.09375*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.885625*height), (0.050625*height)),
                        new CGPoint((0.929375*height), (0.006875*height)),
                        new CGPoint((0.885625*height), (0.050625*height)));
                    _defaultShape.AddLineTo(new CGPoint((0.45375*height), (0.590625*height)));
                    _defaultShape.AddLineTo(new CGPoint((0.26875*height), (0.368125*height)));
                    _defaultShape.AddCurveToPoint(new CGPoint((0.17625*height), (0.368125*height)),
                        new CGPoint((0.26875*height), (0.368125*height)),
                        new CGPoint((0.221875*height), (0.318125*height)));
                    _defaultShape.ClosePath();

                    _defaultShape.MiterLimit = 0;
                }

                return _defaultShape;
            }
        }

        void AutoFitFontToHeight()
        {
            //            CGFloat height = [self heightForCheckbox] * kBoxSize;
            //            CGFloat fontSize = kM13CheckboxMaxFontSize;
            //            CGFloat tempHeight = MAXFLOAT;
            //
            //            do {
            //                //Update font
            //                fontSize -= 1;
            //                UIFont *font = [UIFont fontWithName:_titleLabel.font.fontName size:fontSize];
            //                //Get size
            //                CGSize labelSize;
            //                NSString *text = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //                if ([text respondsToSelector:@selector(sizeWithAttributes:)]) {
            //                    labelSize = [text sizeWithAttributes:@{ NSFontAttributeName: font }];
            //                } else {
            //
            //                    NSDictionary *attributes = @{NSFontAttributeName: font};
            //                    labelSize = [text sizeWithAttributes:attributes];
            //                    labelSize =CGSizeMake(ceilf(labelSize.width), ceilf(labelSize.height));
            //
            //
            //                }
            //                tempHeight = labelSize.height;
            //            } while (tempHeight >= height);
            //
            //            _titleLabel.font = [UIFont fontWithName:_titleLabel.font.fontName size:fontSize];
        }

        void AutoFitWidthToText()
        {
            //                CGSize labelSize;
            //                if ([_titleLabel.text respondsToSelector:@selector(sizeWithAttributes:)]) {
            //                    labelSize = [_titleLabel.text sizeWithAttributes:@{ NSFontAttributeName: _titleLabel.font }];
            //                } else {
            //
            //                    NSDictionary *attributes = @{NSFontAttributeName: _titleLabel.font};
            //                    labelSize = [_titleLabel.text sizeWithAttributes:attributes];
            //                    labelSize =CGSizeMake(ceilf(labelSize.width), ceilf(labelSize.height));
            //                }
            //
            //                self.frame = CGRectMake(
            //                    self.frame.origin.x,
            //                    self.frame.origin.y,
            //                    labelSize.width + ([self heightForCheckbox] * kCheckBoxSpacing) + ((kBoxSize + kCheckHorizontalExtention) * [self heightForCheckbox]),
            //                    self.frame.size.height);
            //                [self layoutSubviews];
        }

        new void LayoutSubviews()
        {
            base.LayoutSubviews();
            var heightForCheckbox = HeightForCheckbox();
            if (CheckAlignment == CheckboxAlignment.Right)
            {
                CheckView.Frame = new CGRect(
                    (TitleLabel.Text.Length == 0
                        ? 0
                        : Frame.Size.Width -
                          ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox)),
                    (Frame.Size.Height - ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox))/
                    2.0,
                    ((Constants.BoxSize + Constants.CheckHorizontalExtention)*heightForCheckbox),
                    heightForCheckbox)
                    .Integral();

                TitleLabel.Frame = new CGRect(
                    0,
                    0,
                    Frame.Size.Width -
                    (heightForCheckbox*
                     (Constants.BoxSize + Constants.CheckHorizontalExtention + Constants.CheckBoxSpacing)),
                    Frame.Size.Height)
                    .Integral();
            }
            else
            {
                //                checkView.frame = CGRectIntegral(CGRectMake(
                //                    0,
                //                    (self.frame.size.height - ((kBoxSize + kCheckHorizontalExtention) * [self heightForCheckbox])) / 2.0,
                //                    ((kBoxSize + kCheckHorizontalExtention) * [self heightForCheckbox]),
                //                    [self heightForCheckbox]));
                //                _titleLabel.frame = CGRectIntegral(CGRectMake(
                //                    checkView.frame.size.width + ([self heightForCheckbox] * kCheckBoxSpacing),
                //                    0,
                //                    self.frame.size.width - ([self heightForCheckbox] * (kBoxSize + kCheckHorizontalExtention + kCheckBoxSpacing)),
                //                    self.frame.size.height));
            }
        }

        public delegate void CheckedChangedEventHandler(object sender, GenericEventArgs<bool> args);

        public event CheckedChangedEventHandler CheckedChanged;

        public void SetCheckState(CheckboxState checkState)
        {
            CheckState = checkState;
            if (CheckedChanged != null)
            {
                CheckedChanged(this, new GenericEventArgs<bool>(CheckState == CheckboxState.Checked));
            }
            CheckView.SetNeedsDisplay();
        }

        void ToggleCheckState()
        {
            if (CheckState == CheckboxState.Unchecked)
            {
                SetCheckState(CheckboxState.Checked);
            }
            else if (CheckState == CheckboxState.Checked)
            {
                SetCheckState(CheckboxState.Unchecked);
            }
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled)
            {
                TitleLabel.TextColor = LabelColor;
            }
            else
            {
                LabelColor = TitleLabel.TextColor;

                nfloat r, g, b, a;
                LabelColor.GetRGBA(out r, out g, out b, out a);
                r = NMath.Floor((nfloat) (r*100.0 + 0.5))/(nfloat) 100.0;
                g = NMath.Floor((nfloat) (g*100.0 + 0.5))/(nfloat) 100.0;
                b = NMath.Floor((nfloat) (b*100.0 + 0.5))/(nfloat) 100.0;
                TitleLabel.TextColor = UIColor.FromRGBA(
                    (r + (nfloat) 0.4),
                    (g + (nfloat) 0.4),
                    (b + (nfloat) 0.4),
                    (nfloat) 1);
            }
            Enabled = enabled;
            CheckView.SetNeedsDisplay();
        }

        //        - (void)setCheckAlignment:(M13CheckboxAlignment)checkAlignment
        //        {
        //            _checkAlignment = checkAlignment;
        //            [self layoutSubviews];
        //        }
        //
        //        - (id)value
        //        {
        //            if (self.checkState == M13CheckboxStateUnchecked) {
        //                return uncheckedValue;
        //            } else if (self.checkState == M13CheckboxStateChecked) {
        //                return checkedValue;
        //            } else {
        //                return mixedValue;
        //            }
        //        }
        //
        //        - (void)setCheckHeight:(CGFloat)checkHeight
        //        {
        //            _checkHeight = checkHeight;
        //            [self layoutSubviews];
        //        }
        //
        //        - (CGRect)checkboxFrame
        //        {
        //            return checkView.frame;
        //        }

        //        public override bool BeginTracking(UITouch uitouch, UIEvent uievent)
        //        {
        //            Debug.WriteLine("Tracking beg");
        //            base.BeginTracking(uitouch, uievent);
        //            CheckView.IsSelected = true;
        //            CheckView.SetNeedsDisplay();
        //            return true;
        //        }
        //
        //
        //        public override bool ContinueTracking(UITouch uitouch, UIEvent uievent)
        //        {
        //            Debug.WriteLine("Tracking cont");
        //            base.ContinueTracking(uitouch, uievent);
        //            return true;
        //        }
        //
        //        public override void EndTracking(UITouch uitouch, UIEvent uievent)
        //        {
        //            Debug.WriteLine("Tracking ended");
        //            CheckView.IsSelected = false;
        //            ToggleCheckState();
        //            SendActionForControlEvents(UIControlEvent.ValueChanged);
        //            // [self sendActionsForControlEvents:UIControlEventValueChanged];
        //            base.EndTracking(uitouch, uievent);
        //        }
        //
        //
        //        public override void CancelTracking(UIEvent uievent)
        //        {
        //            Debug.WriteLine("Tracking cancelled");
        //            CheckView.IsSelected = false;
        //            CheckView.SetNeedsDisplay();
        //            base.CancelTracking(uievent);
        //        }

        UIColor LabelColor { get; set; }

        //float _checkHeight;

        UILabel TitleLabel { get; set; }

        public System.nfloat StrokeWidth { get; set; }

        public UIColor StrokeColor { get; set; }

        public UIColor CheckColor { get; set; }

        public CheckView CheckView { get; set; }

        public CheckboxState CheckState { get; set; }

        public CheckboxAlignment CheckAlignment { get; set; }

        public UIColor UncheckedColor { get; set; }

        public System.nfloat Radius { get; set; }

        //private CGRect _boxFrame;
    }
}
