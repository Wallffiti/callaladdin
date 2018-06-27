using CallAladdin.Commands;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class DisclaimerViewModel : BaseViewModel
    {
        private UserRegistration userRegistration;
        private string disclaimerText;
        private bool isBusy;

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

        public ICommand GoToPersonalDataProtectionCmd { get; set; }

        public DisclaimerViewModel(UserRegistration userRegistration)
        {
            this.UserRegistration = userRegistration;
            this.GoToPersonalDataProtectionCmd = new GoToPersonalDataProtectionCommand(this);

            var strBuilder = new StringBuilder();
            strBuilder.Append("<!DOCTYPE html>")
            .Append("<html>")
            .Append("<head>")
            .Append("<meta charset=utf-8 />")
            .Append("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">")
            .Append("<title>©2017 Call Aladdin Disclaimer</title>")
            .Append("</head>")
            .Append("<body style=\"background-color: #EAECF5\">")
            .Append("<div style=\"background-color: lightgray; padding: 10px; text-align:justify\">")

            .Append("<h3><u>TERMS AND CONDITIONS</u></h3>")
            .Append(" <p>Call Aladdin is a marketplace platform for services performed by its users. The following terms and conditions govern your access to and use of the Call Aladdin website or mobile app, including any content, functionality and services offered on or through [website URL(“thewebsite”) or mobile app name]. Please read the terms and conditions carefully before you start to use the website or mobile app. By using the website or mobile app, you agree to follow and be bound by these terms and conditions in full. If you disagree with these terms and conditions or any part of these terms and conditions, you should not use this website or mobile app.")
            .Append("</p><br/>")

            .Append("<h3><u>DEFINITIONS</u></h3>")
            .Append("<p>“Contractor(s)” are users who offer services through our website or mobile app. “Client(s)” are users who procure the services offered by the Contractors through our website or mobile app.")
            .Append("</p><br/>")

            .Append("<h3><u>OVERVIEW</u></h3>")
            .Append("<p>Only registered users may offer or procure services on our website or mobile app.")
            .Append("</p><br/>")

            .Append("<h3><u>DISCLAIMER</u></h3>")
            .Append("We reserve the right to modify the terms and conditions and to add new or additional terms or conditions on your use of our website or mobile app at any time and at our sole discretion.Such modifications and additional terms and conditions will be effective immediately without notice to you.Your continued use of this website or mobile app will be deemed acceptance thereof. We also reserve the right to terminate your usage of our website or mobile app at any time and at our sole discretion without notice to you.")
            .Append("The information contained in this website or mobile app is for general information purposes only and is not legal advice or other professional advice.While we endeavour to keep the information up to date and correct, we make no representations or warranties of any kind, express or implied, about the completeness, accuracy, reliability, suitability or availability with respect to the website or mobile app or the information, products, services, or related graphics contained on the website or mobile app for any purpose. Any reliance you place on such information is therefore strictly at your own risk.")
            .Append("Nothing on this website or mobile app should be taken to constitute professional advice or formal recommendations and we exclude all representations and warranties relating to the content and use of this website or mobile app.")
            .Append("While the content of this website or mobile app is provided in good faith, we do not warrant that the information will be kept up to date, be true and not misleading, or that this website or mobile app will always(or ever) be available for use.Every effort is made to keep the website or mobile app up and running smoothly.However, we take no responsibility for, and will not be liable for, the website or mobile app being temporarily unavailable due to technical issues beyond our control.")
            .Append("  We do not warrant that the servers that make this website or mobile app available will be error, virus or bug free and you accept that it is your responsibility to make adequate provision for protection against such threats.")
            .Append(" Our website or mobile app makes no express or implied warranty for merchantability, fitness for a particular purpose or otherwise or all other warranties expressed or implied including the warranty of merchantability and the warranty of fitness for a particular purpose are expressly excluded or disclaimed.")
            .Append(" </p><br/>")

            .Append("<h3><u>DISPUTE</u></h3>")
            .Append("<p><b>Our company is a technology company that does not provide or engage in the services")
            .Append(" provided by the Contractors and our company is not a Contractor providing services to Clients.The website or mobile app are intended to be used for facilitating the Contractors to offer their services to the Clients.Our company is not responsible or liable for the acts and / or omissions of any services provided by the Contractors to the Clients, for the acts and / or omissions of payment by the Clients to the Contractors and for any criminal and / or illegal action committed by the Contractors or the Clients.The Contractors shall, at all time, not claim or cause any person to misunderstand that the Contractors are the agent, employee or staff of our company, and the services provided by the Contractors are not, in any way, be deemed as services of our company.")
            .Append(" Furthermore, we are not responsible for the content, quality or the level of service provided by the Contractors.We provide no warranty with respect to the services provided by the Contractors, their delivery, and any communications between Clients and the Contractors.We encourage Clients to take advantage of our rating system, our community and common sense in choosing appropriate service offers.We encourage Clients and Contractors to try and settle conflicts amongst themselves.")
            .Append(" </b></p><br/>")

            .Append(" <h3><u>LIMITATION OF LIABILITY</u></h3>")
            .Append("<p>In no event will we be liable for any incidental, indirect, consequential or special damage of")
            .Append(" any kind, or any damages whatsoever, including, without limitation, those resulting from loss of profit, loss of contracts, goodwill, data, information, income, anticipated savings or business relationships, whether or not advised of the possibility of such damage, arising out of or in connection with the use of this website or mobile app or any linked websites or mobile apps.")
            .Append("</p><br/>")

            .Append("<h3><u>LINKS</u></h3>")
            .Append("<p>This website or mobile app may include links providing direct access to other websites or")
            .Append(" internet resources. We have no control over the nature, content and availability of those websites or internet resources.These links are included for convenience, and the inclusion of any link does not necessarily imply a recommendation or endorse the views expressed within them.We are also not responsible for the accuracy or content of information contained in these websites or internet resources.")
            .Append("  </p><br/>")

            .Append("  <h3><u>USERS</u></h3>")
            .Append(" <p>You may view, download for caching purposes only, and print pages (or other content) from the website or mobile app for your own personal use, but you must not:-")
            .Append(" <ol type=\"i\">")
            .Append(" <li>Republish material from this website or mobile app (including republication on another website);</li>")
            .Append("<li>Sell, rent or sub-licence material from the website or mobile app;</li>")
            .Append("<li>Reproduce, duplicate, copy or otherwise exploit material on our website or mobile app for a commercial purpose;</li>")
            .Append("<li>Edit or otherwise modify any material on the website or mobile app; or</li>")
            .Append("<li>Redistribute material from this website or mobile app.</li>")
            .Append("</ol>")
            .Append("  You must not use our website or mobile app in any way that causes, or may cause damage to the website or impairment of the availability or accessibility of the website, or in any way which is unlawful, illegal, fraudulent or harmful or in connection with any unlawful, illegal, fraudulent or harmful purpose or activity.In case of any violation of the terms and conditions, we reserve the right to seek all remedies available to it in law and in equity.We reserve the right, at our sole discretion and at any time, to terminate your access to the website, mobile app and / or any of its services without notice to you and without liability to you or any third party.")
            .Append("   </p><br/>")

            .Append(" <h3><u>INDEMNITY</u></h3>")
            .Append("<p>By using this website or mobile app, you agree that you shall defend, indemnify and hold our")
            .Append(" company, its licensors and each such party’s parent organizations, subsidiaries, affiliates, officers, directors, members, employees, attorneys and agents harmless from and against any and all claims, costs, damages, losses, liabilities and expenses(including attorneys’ fees and costs) arising out of or in connection with: (a)your violation or breach of any term of this Agreement or any applicable law or regulation, including any local laws or ordinances, whether or not referenced herein; (b)your violation of any rights of any third party, including, but not limited to the Contractors or Clients, as a result of your own interaction with any third party; and(c) your use(or misuse) of the website or mobile app. You agree that you shall not sue or recover any damages from us as a result of our decision to remove or refuse to process any information or content, to warn you, to suspend or terminate your access to our website or mobile app.")
            .Append("</p><br/>")

            .Append(" <h3><u>ENTIRE AGREEMENT</u></h3>")
            .Append(" <p>These terms and conditions constitute the entire agreement between you and us in relation to")
            .Append(" your use of our website or mobile app and any disputes relating to these terms and conditions will be subject to the relevant governing law.")
            .Append("</p><br/>")

            .Append("</div>")
            .Append("</body>")
            .Append("</html>");

            this.DisclaimerText = strBuilder.ToString();
        }

        public async void NavigateToPersonalDataProtection(UserRegistration userRegistration)
        {
            if (isBusy)
            {
                Navigator.Instance.OkAlert("Alert", "The app is currently busy. Please try again later.", "OK", null, null);
                return;
            }

            isBusy = true;
            await Navigator.Instance.NavigateTo(PageType.PERSONAL_DATA_PROTECTION, userRegistration);
            await Task.Delay(1500);
            isBusy = false;
        }
    }
}
