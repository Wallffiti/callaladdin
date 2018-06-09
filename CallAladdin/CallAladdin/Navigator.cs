using CallAladdin.Model;
using CallAladdin.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CallAladdin
{
    public class Navigator
    {
        private static Navigator instance;
        public static Navigator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Navigator();
                }

                return instance;
            }
        }

        private Navigator()
        {

        }

        public CustomPage GetPage(PageType pageType, object parameter)
        {
            CustomPage view = null;

            switch (pageType)
            {
                case PageType.USER_REGISTRATION:
                    view = new UserRegistrationPage() { };
                    break;
                case PageType.USER_LOGIN:
                    view = new UserLoginPage();
                    break;
                case PageType.DISCLAIMER:
                    view = new DisclaimerPage(parameter as UserRegistration);
                    break;
                case PageType.SMS_VERIFICATION:
                    view = new SmsVerificationPage(parameter as UserRegistration);
                    break;
                case PageType.DUMMY:
                    view = new DummyPage();
                    break;
                //case PageType.HOME:
                //    view = new HomePage();
                //    break;
            }

            if (view != null)
            {
                CustomNavigationPage.SetTitlePosition(view, CustomNavigationPage.TitleAlignment.Center);
                CustomNavigationPage.SetTitleFont(view, Font.SystemFontOfSize(NamedSize.Large));
                CustomNavigationPage.SetTitleColor(view, Color.Black);
                CustomNavigationPage.SetTitleFont(view, Font.SystemFontOfSize(20, FontAttributes.Bold));
            }

            return view;
        }

        public async Task NavigateTo(PageType pageType, object parameter = null)
        {
            if (pageType == PageType.HOME)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new HomePage());
                return;
            }
            var view = GetPage(pageType, parameter);
            await App.Current.MainPage.Navigation.PushAsync(view);
        }
    }

    public enum PageType
    {
        USER_REGISTRATION = 0,
        USER_LOGIN = 1,
        DISCLAIMER = 2,
        SMS_VERIFICATION = 3,
        HOME = 4,
        DUMMY = 99
    }
}
