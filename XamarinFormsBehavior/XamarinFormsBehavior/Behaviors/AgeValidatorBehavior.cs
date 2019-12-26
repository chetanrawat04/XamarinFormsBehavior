using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinFormsBehavior.Behaviors
{
    public class AgeValidatorBehavior : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(AgeValidatorBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        static readonly BindablePropertyKey IsFocusPropertyKey = BindableProperty.CreateReadOnly("IsFocus", typeof(bool), typeof(EmailValidatorBehavior), false);
        public static readonly BindableProperty IsFocusProperty = IsFocusPropertyKey.BindableProperty;
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        public bool IsFocus
        {
            get { return (bool)base.GetValue(IsFocusProperty); }
            private set { base.SetValue(IsFocusPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
            bindable.Focused += Bindable_Focused;
            bindable.Unfocused += Bindable_Unfocused;
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text) || IsValid)
                IsFocus = false;
            else
                IsFocus = true;
        }

        private void Bindable_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text) || IsValid)
                IsFocus = false;
            else
                IsFocus = true;
        }
        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
         
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                IsValid = double.Parse(e.NewTextValue) >= 18;
                IsFocus = !IsValid;
            }
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;
        }
    }
}