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

        public Page GetPage(PageType pageType, object parameter, TitleAlignment titleAlignment)
        {
            Page view = null;

            switch (pageType)
            {
                case PageType.USER_REGISTRATION:
                    view = new UserRegistrationPage() { };
                    break;
                case PageType.USER_LOGIN:
                    view = new UserLoginPage(parameter as string);
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
                    view = new HomePage(parameter /*as UserProfile*/);
                    break;
                case PageType.CHANGE_PHONE_NUMBER:
                    view = new ChangePhoneNumberPage(parameter);
                    break;
                case PageType.EDIT_REQUESTOR_PROFILE:
                    view = new EditRequestorProfilePage(parameter);
                    break;
                case PageType.EDIT_CONTRACTOR_PROFILE:
                    view = new EditContractorProfilePage(parameter);
                    break;
                case PageType.JOB_REQUEST:
                    view = new JobRequestPage(parameter);
                    break;
                case PageType.JOB_VIEW:
                    view = new JobViewPage(parameter);
                    break;
                case PageType.HISTORY_JOB_VIEW:
                    view = new JobHistoryViewPage(parameter);
                    break;
                case PageType.EDIT_JOB_VIEW:
                    view = new EditJobPage(parameter);
                    break;
                case PageType.PROFILE_VIEW:
                    view = new ProfilePage(parameter);
                    break;
                case PageType.JOB_ACCEPTANCE_VIEW:
                    view = new JobAcceptanceViewPage(parameter);
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

        public void OkAlert(string title, string message, string okMessage, Action androidAction = null, Action iosAction = null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(title, message, okMessage);

                if (Device.OS == TargetPlatform.Android)
                {
                    androidAction?.Invoke();
                }
                else if (Device.OS == TargetPlatform.iOS)
                {
                    iosAction?.Invoke();
                }
            });
        }

        public void ConfirmationAlert(string title, string message, string confirmMessage, string cancelMessage, Action androidAction = null, Action iosAction = null, Action androidCancelAction = null, Action iosCancelACtion = null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await App.Current.MainPage.DisplayAlert(title, message, confirmMessage, cancelMessage);
                if (result)
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        androidAction?.Invoke();
                    }
                    else if (Device.OS == TargetPlatform.iOS)
                    {
                        iosAction?.Invoke();
                    }
                }
                else
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        androidCancelAction?.Invoke();
                    }
                    else if (Device.OS == TargetPlatform.iOS)
                    {
                        androidCancelAction?.Invoke();
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

        public async Task NavigateToRoot()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
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
        EDIT_REQUESTOR_PROFILE = 7,
        EDIT_CONTRACTOR_PROFILE = 8,
        JOB_REQUEST = 9,
        JOB_VIEW = 10,
        HISTORY_JOB_VIEW = 11,
        EDIT_JOB_VIEW = 12,
        PROFILE_VIEW = 13,
        JOB_ACCEPTANCE_VIEW = 14,
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
