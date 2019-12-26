using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace XamarinFormsBehavior.Behaviors
{
    public class MobileValidatorBehavior : Behavior<Entry>
    {
        const string mobileRegex = @"^[0-9+-]+$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(MobileValidatorBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        static readonly BindablePropertyKey IsFocusPropertyKey = BindableProperty.CreateReadOnly("IsFocus", typeof(bool), typeof(MobileValidatorBehavior), false);
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
            bindable.TextChanged += HandleTextChanged;
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

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, mobileRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            IsFocus = !IsValid;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            bindable.Focused -= Bindable_Focused;
        }
    }
}
