﻿using CallAladdin.Model;
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

        public Page GetPage(PageType pageType, object parameter, TitleAlignment titleAlignment)
        {
            Page view = null;

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
                case PageType.PERSONAL_DATA_PROTECTION:
                    view = new PersonalDataProtectionPage(parameter as UserRegistration);
                    break;
                case PageType.SMS_VERIFICATION:
                    view = new SmsVerificationPage(parameter as UserRegistration);
                    break;
                case PageType.HOME:
                    view = new HomePage();
                    break;
                case PageType.CHANGE_PHONE_NUMBER:
                    view = new ChangePhoneNumberPage(parameter);
                    break;
                case PageType.DUMMY:
                    view = new DummyPage();
                    break;
            }

            if (view != null)
            {
                if (titleAlignment == TitleAlignment.LEFT)
                {
                    CustomNavigationPage.SetTitlePosition(view, CustomNavigationPage.TitleAlignment.Start);
                }
                else if (titleAlignment == TitleAlignment.RIGHT)
                {
                    CustomNavigationPage.SetTitlePosition(view, CustomNavigationPage.TitleAlignment.End);
                }
                else
                {
                    CustomNavigationPage.SetTitlePosition(view, CustomNavigationPage.TitleAlignment.Center);
                }
                
                CustomNavigationPage.SetTitleFont(view, Font.SystemFontOfSize(NamedSize.Large));
                CustomNavigationPage.SetTitleColor(view, Color.Black);
                CustomNavigationPage.SetTitleFont(view, Font.SystemFontOfSize(20, FontAttributes.Bold)); 
            }

            return view;
        }

        public async Task ReturnPrevious(UIPageType currentPageType)
        {
            if (currentPageType == UIPageType.PAGE)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else if (currentPageType == UIPageType.MODAL)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        public void ConfirmationAlert(string title, string message, string okMessage, string cancelMessage, Action androidAction, Action iosAction)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await App.Current.MainPage.DisplayAlert(title, message, okMessage, cancelMessage);
                if (result)
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        androidAction();
                    }
                    else if (Device.OS == TargetPlatform.iOS)
                    {
                        iosAction();
                    }
                }
            });
        }

        public async Task NavigateTo(PageType pageType, object parameter = null, bool appendFromRoot = false, UIPageType uIPageType = UIPageType.PAGE, TitleAlignment titleAlignment = TitleAlignment.CENTER)
        {
            var view = GetPage(pageType, parameter, titleAlignment);

            if (uIPageType == UIPageType.PAGE)
            {
                if (appendFromRoot)
                {
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                    await App.Current.MainPage.Navigation.PushAsync(view);
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(view);
                }
            }
            else if (uIPageType == UIPageType.MODAL)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(view);
            }
        }
    }

    public enum PageType
    {
        USER_REGISTRATION = 0,
        USER_LOGIN = 1,
        DISCLAIMER = 2,
        PERSONAL_DATA_PROTECTION = 3,
        SMS_VERIFICATION = 4,
        HOME = 5,
        CHANGE_PHONE_NUMBER = 6,
        DUMMY = 99
    }

    public enum UIPageType
    {
        PAGE = 0,
        MODAL = 1
    }

    public enum TitleAlignment
    {
        CENTER = 0,
        LEFT = 1,
        RIGHT = 2
    }
}
