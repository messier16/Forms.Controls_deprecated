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

namespace Messier16.Forms.Android.Controls.Native
{

    public class LabelView : View
    {
        public LabelView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitLabelView();
        }

        public LabelView(Context context) : base(context)
        {
            InitLabelView();
        }


        private Paint mTextPaint;
        private String mText;
        private int mAscent;


        private void InitLabelView()
        {
            mText = "Hpla";
            mTextPaint = new Paint();
            mTextPaint.AntiAlias = true;
            mTextPaint.TextSize = 16;
            mTextPaint.Color = new Color(0, 0, 0);
            SetPadding(3, 3, 3, 3);
        }


        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RequestLayout();
                Invalidate();
            }
        }

        public float TextSize
        {
            get { return mTextPaint.TextSize; }
            set
            {
                mTextPaint.TextSize = value;
                RequestLayout();
                Invalidate();
            }
        }

        public Color TextColor
        {
            get { return mTextPaint.Color; }
            set
            {
                mTextPaint.Color = value;
                Invalidate();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {

            SetMeasuredDimension(
                MeasureWidth(widthMeasureSpec),
                MeasureHeight(heightMeasureSpec)
                );
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
                // Measure the text
                result = (int)mTextPaint.MeasureText(mText) + PaddingLeft
                        + PaddingRight;
                if (specMode == MeasureSpecMode.AtMost)
                {
                    // Respect AT_MOST value if that was what is called for by measureSpec
                    result = Math.Min(result, specSize);
                }
            }

            return result;
        }

        private int MeasureHeight(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            int specSize = MeasureSpec.GetSize(measureSpec);

            mAscent = (int)mTextPaint.Ascent();
            if (specMode == MeasureSpecMode.Exactly)
            {
                // We were told how big to be
                result = specSize;
            }
            else
            {
                // Measure the text (beware: ascent is a negative number)
                result = (int)(-mAscent + mTextPaint.Descent()) + PaddingTop
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
            canvas.DrawText(mText, PaddingLeft, PaddingTop - mAscent, mTextPaint);
        }

        /**
         * Render the text
         * 
         * @see android.view.View#onDraw(android.graphics.Canvas)
         * /
        @Override
        protected void onDraw(Canvas canvas)
        {
            super.onDraw(canvas);
            canvas.drawText(mText, getPaddingLeft(), getPaddingTop() - mAscent, mTextPaint);
        }
         */
    }
}