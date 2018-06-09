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
    public class DateValidationBehavior : Behavior<DatePicker>
    {
        protected override void OnAttachedTo(DatePicker datepicker)
        {
            datepicker.DateSelected += Datepicker_DateSelected;
            base.OnAttachedTo(datepicker);
        }

        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime value = e.NewDate;
            bool isValid = Validators.ValidateDate(value);
            ((DatePicker)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(DatePicker datepicker)
        {
            datepicker.DateSelected -= Datepicker_DateSelected;
            base.OnDetachingFrom(datepicker);
        }


    }
}
