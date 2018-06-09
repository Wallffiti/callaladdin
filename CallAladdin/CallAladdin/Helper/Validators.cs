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
    }
}
