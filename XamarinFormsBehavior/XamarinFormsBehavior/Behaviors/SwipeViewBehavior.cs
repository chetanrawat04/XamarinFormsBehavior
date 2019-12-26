using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinFormsBehavior.Behaviors
{
    public class SwipeViewBehavior : Behavior<View>
    {
        View _attachedView;

        public static readonly BindableProperty ScrollDataProperty =
            BindableProperty.Create(nameof(ScrollData),
                typeof(double),
                typeof(SwipeViewBehavior), default(double),
                        propertyChanged: OnScrollDataChanged);

        public double ScrollData
        {
            get { return (double)GetValue(ScrollDataProperty); }
            set { SetValue(ScrollDataProperty, value); }
        }

        static void OnScrollDataChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = (SwipeViewBehavior)bindable;
            instance.ScrollTo((double)newValue);
        }

        void ScrollTo(double newValue)
        {
            var xCalculation = (newValue * -1);

            _attachedView.TranslateTo(xCalculation, 0, 1, Easing.SinIn);
        }

        public SwipeViewBehavior()
        {
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            this._attachedView = bindable;

            bindable.BindingContextChanged += OnAttachedViewBindingContextChanged; ;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            this._attachedView.BindingContextChanged -= OnAttachedViewBindingContextChanged;
        }

        void OnAttachedViewBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = _attachedView.BindingContext;
        }
    }
}