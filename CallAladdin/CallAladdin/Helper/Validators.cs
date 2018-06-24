using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CallAladdin.Helper
{
    public class Validators
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

        public static bool ValidateDate(DateTime value)
        {
            int year = DateTime.Now.Year;
            int selyear = value.Year;
            int result = selyear - year;
            bool isValid = false;
            if (result <= 100 && result > 0)
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool ValidateEmail(string email)
        {
            return (Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        public static bool ValidatePassword(string password)
        {
            return (Regex.IsMatch(password, passwordRegex));
        }

        public static bool ValidateNumber(string number)
        {
            int result;
            return int.TryParse(number, out result);
        }

        public static bool ValidateUrl(string uriName)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        public static bool TimeSelectionIsValid(DateTime selectedStartDate, TimeSpan selectedStartTime, DateTime selectedEndDate, TimeSpan selectedEndTime)
        {
            if (selectedStartDate == null || selectedStartTime == null || selectedEndDate == null || selectedEndTime == null)
                return false;

            var start = new DateTime(selectedStartDate.Year, selectedStartDate.Month, selectedStartDate.Day, selectedStartTime.Hours, selectedStartTime.Minutes, selectedStartTime.Seconds);
            var end = new DateTime(selectedEndDate.Year, selectedEndDate.Month, selectedEndDate.Day, selectedEndTime.Hours, selectedEndTime.Minutes, selectedEndTime.Seconds);
            var timeIsValid = end > start;
            return timeIsValid;
        }
    }
}
