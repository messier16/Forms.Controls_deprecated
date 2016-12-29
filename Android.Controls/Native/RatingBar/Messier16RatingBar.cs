using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace Messier16.Forms.Android.Controls.Native.RatingBar
{

    public class Messier16RatingBar : View
    {

        private int _maxStar = 5;

        public int MaxStars
        {
            get { return _maxStar; }
            set { _maxStar = value; }
        }

        private float _padding = 2;

        public float Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }

        public Messier16RatingBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitLabelView();
        }

        public Messier16RatingBar(Context context) : base(context)
        {
            InitLabelView();
        }


        private Paint fillPaint;
        private String mText;
        private int mAscent;


        private void InitLabelView()
        {
            mText = "Hpla";
            fillPaint = new Paint();
            fillPaint.AntiAlias = true;
            fillPaint.TextSize = 16;
            fillPaint.Color = new Color(0, 0, 0);
            SetPadding(3, 3, 3, 3);
        }

        public Color FillColor
        {
            get { return fillPaint.Color; }
            set
            {
                fillPaint.Color = value;
                Invalidate();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension(MeasureWidth(widthMeasureSpec),MeasureHeight(heightMeasureSpec));
        }

        private int MeasureWidth(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                // We were told how big to be
                result = specSize;
            }
            else
            {
                //// Measure the text
                //result = (int)mTextPaint.MeasureText(mText) + PaddingLeft
                //        + PaddingRight;
                //if (specMode == MeasureSpecMode.AtMost)
                //{
                //    result = Math.Min(result, specSize);
                //}
            }

            return result;
        }

        private int MeasureHeight(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            mAscent = (int)fillPaint.Ascent();
            if (specMode == MeasureSpecMode.Exactly)
            {
                // We were told how big to be
                result = specSize;
            }
            else
            {
                // Measure the text (beware: ascent is a negative number)
                result = (int)(-mAscent + fillPaint.Descent()) + PaddingTop
                        + PaddingBottom;
                if (specMode == MeasureSpecMode.AtMost)
                {
                    // Respect AT_MOST value if that was what is called for by measureSpec
                    result = Math.Min(result, specSize);
                }
            }
            return result;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            float starSizeWidth = canvas.Width / MaxStars;
            float starSizeHeight = canvas.Height;

            var trueStarSize = Math.Min(starSizeHeight, starSizeWidth);

            var starCenter = starSizeWidth / (float)2;

            //System.Diagnostics.Debug.WriteLine($"Star size {trueStarSize} W:{starSizeWidth} H:{starSizeHeight}");
            var rating = Rating;
            System.Diagnostics.Debug.Write($"{Rating}");
            for (int i = 0; i < MaxStars; i++)
            {
                var middle = starSizeWidth * i + starSizeWidth / 2;
                var left = middle - trueStarSize / 2;

                var boundingBox = new RectF(left + Padding, 0, left + trueStarSize - Padding, trueStarSize - Padding);
                canvas.DrawRoundRect(boundingBox, 10, 10, fillPaint);

                RectF fillBox = null;
                if ((i + 1) < Rating)
                {
                    fillBox = new RectF(left, 0, left + trueStarSize, trueStarSize);
                }
                else if (0 < Rating - i)
                {
                    fillBox = new RectF(left, 0, left + trueStarSize * (Rating - i), trueStarSize);
                }
                if (fillBox != null)
                    canvas.DrawRoundRect(fillBox, 30, 30, new Paint() { Color = Color.Red });
            }
        }

        private const float TOUCH_SCALE_FACTOR = 180.0f / 320;
        public override bool OnTouchEvent(MotionEvent e)
        {
            float x = e.GetX();
            float y = e.GetY();
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    CalculateNewRating(x);
                    break;
                case MotionEventActions.Move:
                    CalculateNewRating(x);
                    break;
                default:
                    break;
            }
            return true;
        }

        float Rating;
        void CalculateNewRating(float movement)
        {
            Rating = MaxStars * (movement / Width);

            Invalidate();
        }
    }
}