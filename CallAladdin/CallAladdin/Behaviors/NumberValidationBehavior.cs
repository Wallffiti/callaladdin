using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CallAladdin.Helper;

namespace CallAladdin.Behaviors
{
    /// <summary>
    /// Refer https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    /// </summary>
    public class NumberValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;

            bool isValid = Validators.ValidateNumber(args.NewTextValue); //int.TryParse(args.NewTextValue, out result);

            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
