using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;


namespace Messier16.Forms.iOS.Controls.Native.Checkbox
{

    [Register("Checkbox"), DesignTimeVisible(true)]
    public class Checkbox : UIButton
    {

        public Checkbox(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public Checkbox(CGRect frame) : base(frame)
        {
            Initialize();
        }

        void Initialize()
        {
            base.BackgroundColor = UIColor.Clear;


        }

        bool _touching = false;

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            if (Enabled)
                _touching = true;
            else
                _touching = false;
        }
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            if (_touching && Enabled)
                Checked = !Checked;
            _touching = false;
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            _touching = false;
        }

        public event CheckedChangedEventHandler CheckedChanged;

        bool _checked;
        [Export("Checked"), Browsable(true)]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(value));
                    SetNeedsDisplay();
                }
            }
        }

        UIColor _tickColor = UIColor.LightGray;
        [Export("TickColor"), Browsable(true)]
        public UIColor TickColor
        {
            get { return _tickColor; }
            set { _tickColor = value; SetNeedsDisplay(); }
        }

        UIColor _checkboxBackgroundColor = UIColor.DarkGray;
        [Export("CheckboxBackgroundColor"), Browsable(true)]
        public UIColor CheckboxBackgroundColor
        {
            get { return _checkboxBackgroundColor; }
            set { _checkboxBackgroundColor = value; SetNeedsDisplay(); }
        }

        //bool _enabled = false;
        //[Export("Enabled"), Browsable(true)]
        //public bool Enabled
        //{
        //    get { return _enabled; }
        //    set { _enabled = value; SetNeedsDisplay(); }
        //}
        
        //[Export("Hidden"), Browsable(true)]
        //public new bool Hidden
        //{
        //    get { return base.Hidden; }
        //    set { base.Hidden = value; SetNeedsDisplay(); }
        //}

        public override void Draw(CoreGraphics.CGRect rect)
        {
            //if(!Hidden)
            DrawCheck(Frame);
        }

        private void DrawCheck(CGRect frame)
        {
            var bgColor = Enabled ? CheckboxBackgroundColor : CheckboxBackgroundColor.Lighter(20);
            var tickColor = Enabled ? TickColor : TickColor.Lighter(20);
            var size = Math.Min(frame.Width, frame.Height);
            var radius = (float)(size * 0.1);
            frame = new CGRect(frame.Location, new CGSize(size, size));


            //// Outer Drawing
            var outerPath = UIBezierPath.FromRoundedRect(new CGRect(frame.GetMinX() + NMath.Floor(frame.Width * 0.05000f) + 0.5f, frame.GetMinY() + NMath.Floor(frame.Height * 0.05000f) + 0.5f, NMath.Floor(frame.Width * 0.95000f) - NMath.Floor(frame.Width * 0.05000f), NMath.Floor(frame.Height * 0.95000f) - NMath.Floor(frame.Height * 0.05000f)), radius);
            bgColor.SetFill();
            outerPath.Fill();

            if (Checked)
            {
                //// Check Drawing
                var checkPath = new UIBezierPath();
                checkPath.MoveTo(new CGPoint(frame.GetMinX() + 0.76208f * frame.Width, frame.GetMinY() + 0.26000f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.38805f * frame.Width, frame.GetMinY() + 0.62670f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.23230f * frame.Width, frame.GetMinY() + 0.47400f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.18000f * frame.Width, frame.GetMinY() + 0.52527f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.36190f * frame.Width, frame.GetMinY() + 0.70360f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.38805f * frame.Width, frame.GetMinY() + 0.72813f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.41420f * frame.Width, frame.GetMinY() + 0.70360f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.81437f * frame.Width, frame.GetMinY() + 0.31127f * frame.Height));
                checkPath.AddLineTo(new CGPoint(frame.GetMinX() + 0.76208f * frame.Width, frame.GetMinY() + 0.26000f * frame.Height));
                checkPath.ClosePath();
                tickColor.SetFill();
                checkPath.Fill();

            }
        }

    }

    public delegate void CheckedChangedEventHandler(object sender, CheckedChangedEventArgs e);

    [System.Serializable]
    public sealed class CheckedChangedEventArgs : EventArgs
    {
        public bool Checked
        {
            get;
            private set;
        }
        public CheckedChangedEventArgs(bool chk)
        {
            Checked = chk;
        }
    }

}
