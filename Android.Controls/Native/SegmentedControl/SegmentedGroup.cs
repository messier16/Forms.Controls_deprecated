using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;

namespace Messier16.Forms.Android.Controls.Native.SegmentedControl
{
    public class SegmentedGroup : RadioGroup
    {
        private int mMarginDp;
        private Resources resources;
        private int mTintColor;
        private int mUnCheckedTintColor;
        private int mCheckedTextColor = Color.White;
        //private LayoutSelector mLayoutSelector;
        LayoutSelector mLayoutSelector;
        private Float mCornerRadius;
        //private OnCheckedChangeListener mCheckedChangeListener;
        //private HashMap<Integer, TransitionDrawable> mDrawableMap;
        private HashMap /*<Integer, TransitionDrawable> */ mDrawableMap;
        private int mLastCheckId;


        public SegmentedGroup(Context context) : base(context)
        {
            //resources = Resources;
            mTintColor = 0; // resources.GetColor getColor(R.color.radio_button_selected_color);
            mUnCheckedTintColor = 0; //= resources.getColor(R.color.radio_button_unselected_color);
            mMarginDp = 10; //(int)getResources().getDimension(R.dimen.radio_button_stroke_border);
            mCornerRadius = (Float) 5; // getResources().getDimension(R.dimen.radio_button_conner_radius);
            //mLayoutSelector = new LayoutSelector(mCornerRadius);
        }

        public SegmentedGroup(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            //resources = getResources();
            //mTintColor = resources.getColor(R.color.radio_button_selected_color);
            //mUnCheckedTintColor = resources.getColor(R.color.radio_button_unselected_color);
            //mMarginDp = (int)getResources().getDimension(R.dimen.radio_button_stroke_border);
            //mCornerRadius = getResources().getDimension(R.dimen.radio_button_conner_radius);
            InitAttrs(attrs);
            //mLayoutSelector = new LayoutSelector(mCornerRadius);
        }

        /* Reads the attributes from the layout */

        private void InitAttrs(IAttributeSet attrs)
        {
            //TypedArray typedArray = getContext().getTheme().obtainStyledAttributes(
            //        attrs,
            //        R.styleable.SegmentedGroup,
            //        0, 0);

            //try
            //{
            //    mMarginDp = (int)typedArray.getDimension(
            //            R.styleable.SegmentedGroup_sc_border_width,
            //            getResources().getDimension(R.dimen.radio_button_stroke_border));

            //    mCornerRadius = typedArray.getDimension(
            //            R.styleable.SegmentedGroup_sc_corner_radius,
            //            getResources().getDimension(R.dimen.radio_button_conner_radius));

            //    mTintColor = typedArray.getColor(
            //            R.styleable.SegmentedGroup_sc_tint_color,
            //            getResources().getColor(R.color.radio_button_selected_color));

            //    mCheckedTextColor = typedArray.getColor(
            //            R.styleable.SegmentedGroup_sc_checked_text_color,
            //            getResources().getColor(android.R.color.white));

            //    mUnCheckedTintColor = typedArray.getColor(
            //            R.styleable.SegmentedGroup_sc_unchecked_tint_color,
            //            getResources().getColor(R.color.radio_button_unselected_color));
            //}
            //finally
            //{
            //    typedArray.recycle();
            //}
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();
            updateBackground();
        }

        public void setTintColor(int tintColor)
        {
            mTintColor = tintColor;
            updateBackground();
        }

        public void setTintColor(int tintColor, int checkedTextColor)
        {
            mTintColor = tintColor;
            mCheckedTextColor = checkedTextColor;
            updateBackground();
        }

        public void setUnCheckedTintColor(int unCheckedTintColor, int unCheckedTextColor)
        {
            mUnCheckedTintColor = unCheckedTintColor;
            updateBackground();
        }

        public void updateBackground()
        {
            mDrawableMap = new HashMap();
            int count = ChildCount;
            for (int i = 0; i < count; i++)
            {
                View child = GetChildAt(i);
                updateBackground(child);

                // If this is the last view, don't set LayoutParams
                if (i == count - 1) break;

                LayoutParams initParams = (LayoutParams) child.LayoutParameters;
                LayoutParams p = new LayoutParams(initParams.Width, initParams.Height, initParams.Weight);
                // Check orientation for proper margins
                if (Orientation == global::Android.Widget.Orientation.Horizontal)
                {
                    p.SetMargins(0, 0, -mMarginDp, 0);
                }
                else
                {
                    p.SetMargins(0, 0, 0, -mMarginDp);
                }
                child.LayoutParameters = p;
            }
        }

        private void updateBackground(View view)
        {
            int chk = mLayoutSelector.getSelected();
            int unchk = mLayoutSelector.getUnselected();
            //Set text color

            ColorStateList colorStateList = new ColorStateList(new int[][]
            {
                {-android.R.attr.state_checked},
                {android.R.attr.state_checked}
            },
                new int[] {mTintColor, mCheckedTextColor});
            ((Button) view).setTextColor(colorStateList);

            //Redraw with tint color
            Drawable checkedDrawable = resources.getDrawable(checked).mutate();
            Drawable uncheckedDrawable = resources.getDrawable(unchecked).mutate();
            ((GradientDrawable) checkedDrawable).setColor(mTintColor);
            ((GradientDrawable) checkedDrawable).setStroke(mMarginDp, mTintColor);
            ((GradientDrawable) uncheckedDrawable).setStroke(mMarginDp, mTintColor);
            ((GradientDrawable) uncheckedDrawable).setColor(mUnCheckedTintColor);
            //Set proper radius
            ((GradientDrawable) checkedDrawable).setCornerRadii(mLayoutSelector.getChildRadii(view));
            ((GradientDrawable) uncheckedDrawable).setCornerRadii(mLayoutSelector.getChildRadii(view));

            GradientDrawable maskDrawable = (GradientDrawable) resources.getDrawable(unchecked).mutate();
            maskDrawable.setStroke(mMarginDp, mTintColor);
            maskDrawable.setColor(mUnCheckedTintColor);
            maskDrawable.setCornerRadii(mLayoutSelector.getChildRadii(view));
            int maskColor = Color.argb(50, Color.red(mTintColor), Color.green(mTintColor), Color.blue(mTintColor));
            maskDrawable.setColor(maskColor);
            LayerDrawable pressedDrawable = new LayerDrawable(new Drawable[] {uncheckedDrawable, maskDrawable});

            Drawable[] drawables = {uncheckedDrawable, checkedDrawable};
            TransitionDrawable transitionDrawable = new TransitionDrawable(drawables);
            if (((RadioButton) view).IsChecked)
            {
                transitionDrawable.ReverseTransition(0);
            }

            StateListDrawable stateListDrawable = new StateListDrawable();
            stateListDrawable.AddState(new int[] {-android.R.attr.state_checked, android.R.attr.state_pressed},
                pressedDrawable);
            stateListDrawable.AddState(StateSet.WildCard, transitionDrawable);

            mDrawableMap.put(view.getId(), transitionDrawable);

            //Set button background
            if (Build.VERSION.SDK_INT >= 16)
            {
                view.SetBackgroundDrawable(stateListDrawable);
            }
            else
            {
                view.SetBackgroundDrawable(stateListDrawable);
            }

            super.setOnCheckedChangeListener(new OnCheckedChangeListener()
            {
                @Override
            public void onCheckedChanged(RadioGroup group,
                int checkedId)
{
    TransitionDrawable current = mDrawableMap.get(checkedId);
    current.ReverseTransition(200);
    if (mLastCheckId != 0)
{
    TransitionDrawable last = mDrawableMap.get(mLastCheckId);
    if (last != null) last.reverseTransition(200);
            }
            mLastCheckId = checkedId;

            if (mCheckedChangeListener != null)
            {
                mCheckedChangeListener.onCheckedChanged(group, checkedId);
            }
        }
        }
        )
            ;
        }

        public override void OnViewRemoved(View child)
        {
            super.onViewRemoved(child);
            mDrawableMap.remove(child.getId());
        }

        //@Override

    
        //public void setOnCheckedChangeListener(OnCheckedChangeListener listener)
        //{
        //    mCheckedChangeListener = listener;
        //}

/*
     * This class is used to provide the proper layout based on the view.
     * Also provides the proper radius for corners.
     * The layout is the same for each selected left/top middle or right/bottom button.
     * float tables for setting the radius via Gradient.setCornerRadii are used instead
     * of multiple xml drawables.
     */

        private class LayoutSelector
        {

            private int children;
            private int child;
            private const int SELECTED_LAYOUT = 0; // R.drawable.radio_checked;
            private const int UNSELECTED_LAYOUT = 0; //R.drawable.radio_unchecked;

            private float r; //this is the radios read by attributes or xml dimens

            private /*final*/ float r1 = 0f;
            // TypedValue.ApplyDimension(ComplexUnitType.Dip, 0.1f,  getResources().getDisplayMetrics());    //0.1 dp to px

            private /*final*/ float[] rLeft; // left radio button
            private /*final*/ float[] rRight; // right radio button
            private /*final*/ float[] rMiddle; // middle radio button
            private /*final*/ float[] rDefault; // default radio button
            private /*final*/ float[] rTop; // top radio button
            private /*final*/ float[] rBot; // bot radio button
            private float[] radii; // result radii float table

            public LayoutSelector(float cornerRadius)
            {
                children = -1; // Init this to force setChildRadii() to enter for the first time.
                child = -1; // Init this to force setChildRadii() to enter for the first time
                r = cornerRadius;
                rLeft = new float[] {r, r, r1, r1, r1, r1, r, r};
                rRight = new float[] {r1, r1, r, r, r, r, r1, r1};
                rMiddle = new float[] {r1, r1, r1, r1, r1, r1, r1, r1};
                rDefault = new float[] {r, r, r, r, r, r, r, r};
                rTop = new float[] {r, r, r, r, r1, r1, r1, r1};
                rBot = new float[] {r1, r1, r1, r1, r, r, r, r};
            }

            private int GetChildren()
            {
                return 0; //Seg SegmentedGroup.this.getChildCount();
            }

            private int GetChildIndex(View view)
            {
                return 0; //SegmentedGroup.this.indexOfChild(view);
            }

            private void SetChildRadii(int newChildren, int newChild)
            {

                // If same values are passed, just return. No need to update anything
                if (children == newChildren && child == newChild)
                    return;

                // Set the new values
                children = newChildren;
                child = newChild;

                // if there is only one child provide the default radio button
                if (children == 1)
                {
                    radii = rDefault;
                }
                else if (child == 0)
                {
                    //left or top
                    radii = (getOrientation() == LinearLayout.HORIZONTAL) ? rLeft : rTop;
                }
                else if (child == children - 1)
                {
                    //right or bottom
                    radii = (getOrientation() == LinearLayout.HORIZONTAL) ? rRight : rBot;
                }
                else
                {
                    //middle
                    radii = rMiddle;
                }
            }

            /* Returns the selected layout id based on view */

            public int getSelected()
            {
                return SELECTED_LAYOUT;
            }

            /* Returns the unselected layout id based on view */

            public int getUnselected()
            {
                return UNSELECTED_LAYOUT;
            }

            /* Returns the radii float table based on view for Gradient.setRadii()*/

            public float[] getChildRadii(View view)
            {
                int newChildren = getChildren();
                int newChild = getChildIndex(view);
                setChildRadii(newChildren, newChild);
                return radii;
            }
        }
    }

}
