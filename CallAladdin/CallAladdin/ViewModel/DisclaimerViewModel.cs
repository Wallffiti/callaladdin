using CallAladdin.Commands;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class DisclaimerViewModel : BaseViewModel
    {
        private UserRegistration userRegistration;
        private string disclaimerText;

        public string DisclaimerText
        {
            get
            {
                return disclaimerText;
            }
            set
            {
                disclaimerText = value;
                OnPropertyChanged("DisclaimerText");
            }
        }

        public UserRegistration UserRegistration
        {
            get { return userRegistration; }
            set
            {
                userRegistration = value;
                OnPropertyChanged("UserRegistration");
            }
        }

        public ICommand GoToVerificationCmd { get; set; }

        public DisclaimerViewModel(UserRegistration userRegistration)
        {
            this.UserRegistration = userRegistration;
            this.GoToVerificationCmd = new GoToVerificationCommand(this);

            var strBuilder = new StringBuilder();
            strBuilder.Append("<!DOCTYPE html>")
            .Append("<html>")
            .Append("<head>")
            .Append("<meta charset=utf-8 />")
            .Append("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">")
            .Append("<title>©2017 Call Aladdin Disclaimer</title>")
            .Append("</head>")
            .Append("<body>")
            .Append("<div>")
            .Append("<h3><u>TERMS AND CONDITIONS</u></h3>")
            .Append(" <p>Call Aladdin is a marketplace platform for services performed by its users. The following terms and conditions govern your access to and use of the Call Aladdin website or mobile app, including any content, functionality and services offered on or through [website URL(“thewebsite”) or mobile app name]. Please read the terms and conditions carefully before you start to use the website or mobile app. By using the website or mobile app, you agree to follow and be bound by these terms and conditions in full. If you disagree with these terms and conditions or any part of these terms and conditions, you should not use this website or mobile app.")
            .Append("</p><br/>")
            .Append("<h3><u>DEFINITIONS</u></h3>")
            .Append("<p>“Contractor(s)” are users who offer services through our website or mobile app. “Client(s)” are users who procure the services offered by the Contractors through our website or mobile app.")
            .Append("</p><br/>")
            .Append("<h3><u>DISCLAIMER</u></h3>")
            .Append("<p>Only registered users may offer or procure services on our website or mobile app.")
            .Append("</p><br/>")

            .Append("</div>")
            .Append("</body>")
            .Append("</html>");

            this.DisclaimerText = strBuilder.ToString();
        }
    }
}
