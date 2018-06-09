using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using CallAladdin.Helper;

namespace CallAladdin.Behaviors
{
    /// <summary>
    /// Refer https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    /// </summary>
    public class PasswordValidationBehavior : Behavior<Entry>
    {
        //const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = Validators.ValidatePassword(e.NewTextValue); //(Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
