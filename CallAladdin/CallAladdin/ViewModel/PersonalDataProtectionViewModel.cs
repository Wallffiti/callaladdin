using CallAladdin.Commands;
using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class PersonalDataProtectionViewModel : BaseViewModel
    {
        private IUserService userService;
        private UserRegistration userRegistration;
        public ICommand GoToVerificationCmd { get; set; }
        public event EventHandler AcceptAgreementEvent;
        private string personalDataProtectionText;
        private bool isBusy;

        //public UserRegistration UserRegistration
        //{
        //    get { return userRegistration; }
        //    set
        //    {
        //        userRegistration = value;
        //        OnPropertyChanged("UserRegistration");
        //    }
        //}

        public string PersonalDataProtectionText
        {
            get { return personalDataProtectionText; }
            set { personalDataProtectionText = value; OnPropertyChanged("PersonalDataProtectionText"); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public PersonalDataProtectionViewModel(UserRegistration userRegistration)
        {
            this.userService = new UserService();
            this.userRegistration = userRegistration;
            GoToVerificationCmd = new GoToVerificationCommand(this);
            var strBuilder = new StringBuilder();
            strBuilder.Append("<!DOCTYPE html>")
            .Append("<html>")
            .Append("<head>")
            .Append("<meta charset=utf-8 />")
            .Append("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">")
            .Append("<title>©2017 Call Aladdin Disclaimer</title>")
            .Append("</head>")
            .Append("<body style=\"background-color: #EAECF5\">")
            .Append("<div style=\"background-color: lightgray; padding: 10px\">")

            //TODO for body
            .Append(" <h3><u>PERSONAL DATA PROTECTION</u></h3>")
            .Append(" <p>You agree and consent to the Company using and processing your Personal Data for the Purposes and in the manner as identified hereunder.</p>")
            .Append(" <p>The Company views your Personal Data and privacy seriously. For the purposes of this Agreement, “Personal Data” means information about you, from which you are identifiable, including but not limited to your name, identification card number, birth certificate number, passport number, nationality, address, telephone number, credit or debit card details, race, gender, date of birth, email address, any information about you which you have provided to the Company in registration forms, application forms or any other similar forms and / or any information about you that has been or may be collected, stored, used and processed by the Company from time to time and includes sensitive personal data such as data relating to health, religious or other similar beliefs.</p>")
            .Append("<p>The provision of your Personal Data is voluntary. However if you do not provide the Company your Personal Data, your request for the Application may be incomplete and the Company will not be able to process your Personal Data for the Purposes outlined below and may cause the Company to be unable to allow you to use our services.Your continued usage of our services is deemed consent for the Company to collect, process and store the data.Failure to consent to the above may result in the Company being unable to continue or provide the services or facilities to you.</p>")
            .Append("<p>The Company may use and process your Personal Data for business and activities of the Company which shall include, without limitation the following(“the Purpose”):</p>")

            .Append("<ul>")
            .Append("  <li>To perform the Company’s obligations in respect of any arrangements entered into with you;</li>")
            .Append("   <li>To provide you with any services pursuant to the Terms and Conditions herein;</li>")
            .Append("<li>To process your participation in any events, promotions, activities, focus groups, research studies, contests, promotions, polls, surveys or any productions and to communicate with you regarding your attendance thereto;</li>")
            .Append("<li>Process, manage or verify your application for our services pursuant to the Terms and Conditions herein;</li>")
            .Append("<li>To validate and/or process payments (if any) pursuant to the Terms and Conditions herein;</li>")
            .Append("<li>To develop, enhance and provide what is required pursuant to the Terms and Conditions herein to meet your needs;</li>")
            .Append("<li>To process any refunds, rebates and/or charges pursuant to the Terms and Conditions herein;</li>")
            .Append("<li>To facilitate or enable any checks as may be required pursuant to the Terms and Conditions herein;</li>")
            .Append("<li>To respond to questions, comments and feedback from you;</li>")
            .Append("<li>To communicate with you for any of the purposes listed herein;</li>")
            .Append("<li>For internal administrative purposes, such as auditing, data analysis, database records;</li>")
            .Append("<li>For purposes of detection, prevention and prosecution of crime;</li>")
            .Append("<li>For the Company to comply with its obligations under law;</li>")
            .Append("<li>To send you alerts, newsletters, updates, mailers, promotional materials, special privileges, festive greetings from the Company, its partners, advertisers and / or sponsors;</li>")
            .Append("<li>To notify and invite you to events or activities organised by the Company, its partners, advertisers, and/or sponsors;</li>")
            .Append("<li>To share your Personal Data amongst the companies within the Company’s group of companies (if any) comprising the subsidiaries, associate companies and / or jointly controlled entities of the holding company of the group (“the Group”) and with the Company’s and Group’s agents, third party providers, developers, advertisers, partners, event companies or sponsors who may communicate with you for any reasons whatsoever.</li>")
            .Append("</ul>")

            .Append("<p>The Company may share Personal Data with companies who provide services such as information processing, extending credit, fulfilling customer orders, delivering products to you, managing and enhancing customer data, providing customer service, assessing your interest in our products and services, and conducting customer research or satisfaction surveys.These companies are obligated to protect your information.</p>")
            .Append(" <p>It may be necessary − by law, legal process, litigation, and/or requests from public and governmental authorities within or outside your country of residence − for the Company to disclose your personal information.We may also disclose information about you if we determine that for purposes of national security, law enforcement, or other issues of public importance, disclosure is necessary or appropriate.</p>")
            .Append("<p>We may also disclose information about you if we determine that disclosure is reasonably necessary to enforce our terms and conditions or protect our operations or users.Additionally, in the event of a reorganization, merger, or sale we may transfer any and all personal information we collect to the relevant third party.</p>")
            .Append("<p>If you do not consent to the Company processing your Personal Data for any of the Purposes, please notify the Company using the support contact details as provided in the application.</p>")
            .Append("<p>If any of the Personal Data that you have provided to us changes, for example, if you change your e- mail address, telephone number, payment details or if you wish to cancel your account, please update your details by sending your request to the support contact details as provided in the application. We may decline to process requests that are frivolous / vexatious, jeopardize the privacy of others, are extremely impractical, or for which access is not otherwise required by local law.</p>")
            .Append("<p>We will, to the best of our abilities, effect such changes as requested within fourteen (14) working days of receipt of such notice of change.</p> ")
            .Append(" <p>By submitting your information you consent to the use of that information as set out in the form of submission.</p>")

            .Append("</div>")
            .Append("</body>")
            .Append("</html>");

            this.PersonalDataProtectionText = strBuilder.ToString();
        }

        public async void NavigateToSmsVerification(/*UserRegistration userRegistration*/)
        {
            await Navigator.Instance.NavigateTo(PageType.SMS_VERIFICATION, this.userRegistration);
        }

        public async void NavigateToHome(/*UserRegistration userRegistration*/)
        {
            IsBusy = true;

            //1. Sign up via firebase auth
            var signupUserResponse = await userService.RegisterUserToAuthServer(this.userRegistration);

            if (signupUserResponse != null)
            {
                if (signupUserResponse.IsError)
                {
                    Navigator.Instance.OkAlert("Alert", "There is a problem with the registration process. Error: " + signupUserResponse.ErrorMessage, "OK", () =>
                    {
                        //For android
                    }, () =>
                    {
                        //For ios
                    });

                    IsBusy = false;

                    return;
                }

                //2. Create user via backend server
                var createUserResponse = await userService.CreateUser(this.userRegistration);

                if (createUserResponse != null)
                {
                    //TODO: save jwt and local id into local storage

                    Navigator.Instance.OkAlert("Successful", "Thank you for registrating with us. You can now use Call Aladdin.", "OK", async () =>
                    {
                        //For android
                        await Navigator.Instance.NavigateTo(PageType.HOME/*, userRegistration*/);
                        IsBusy = false;
                    }, async () =>
                    {
                        //For ios
                        await Navigator.Instance.NavigateTo(PageType.HOME/*, userRegistration*/);
                        IsBusy = false;
                    });

                    return;
                }
            }

            Navigator.Instance.OkAlert("Alert", "There is a problem with the registration process. Please try again later.", "OK", () =>
            {
                //For android
            }, () =>
            {
                //For ios
            });

            IsBusy = false;
        }

        public void NotifyViewOnConfirmation()
        {
            if (this.AcceptAgreementEvent != null)
            {
                this.AcceptAgreementEvent(this, null);
            }
        }
    }
}
