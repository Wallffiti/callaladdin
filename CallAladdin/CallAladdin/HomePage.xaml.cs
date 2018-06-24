using CallAladdin.EventArgs;
using CallAladdin.Model;
using CallAladdin.Observers.Interfaces;
using CallAladdin.Renderers;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallAladdin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ISubscriber //: CustomPage
    {
        private HomeViewModel homeViewModel;

        public HomePage(object owner)
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel(owner);
            BindingContext = homeViewModel;
            homeViewModel.SubscribeMeToThis(this);
            TabbedPage tabbedPage = GetTabbedPage();
            if (tabbedPage != null)
            {
                tabbedPage.CurrentPageChanged += TabbedPage_CurrentPageChanged;
            }
        }

        private TabbedPage GetTabbedPage()
        {
            return this as TabbedPage;
        }

        private void TabbedPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            var tabbedPage = GetTabbedPage();
            if (tabbedPage != null)
            {
                if (tabbedPage.CurrentPage == GetContentPage(Constants.DASHBOARD) /*tabbedPage.Children[1]*/)
                {
                    Task.Run(async () =>
                    {
                        await homeViewModel.RefreshDashboardViewAsync();
                    });
                }
                else if (tabbedPage.CurrentPage == GetContentPage(Constants.CONTRACTOR) /* tabbedPage.Children[2]*/)
                {
                    Task.Run(async () =>
                    {
                        await homeViewModel.RefreshContractorViewAsync();
                    });
                }
                else if (tabbedPage.CurrentPage == GetContentPage(Constants.HISTORY) /*tabbedPage.Children[3]*/)
                {
                    Task.Run(async () =>
                    {
                        await homeViewModel.RefreshHistoryViewAsync(); ;
                    });
                }
            }
        }

        public void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs)
        {
            //if needed
        }

        private ContentPage GetContentPage(string title)
        {
            ContentPage result = null;
            var tabbedPage = GetTabbedPage();

            if (tabbedPage != null && tabbedPage.Children.Count > 0)
            {
                switch (title)
                {
                    case Constants.HOME:
                        result = tabbedPage.Children[0] as ContentPage;
                        break;
                    case Constants.DASHBOARD:
                        result = tabbedPage.Children[1] as ContentPage;
                        break;
                    case Constants.CONTRACTOR:
                        result = tabbedPage.Children[2] as ContentPage;
                        break;
                    case Constants.HISTORY:
                        result = tabbedPage.Children[3] as ContentPage;
                        break;
                    case Constants.USER_PROFILE:
                        result = tabbedPage.Children[4] as ContentPage;
                        break;
                }
            }

            return result;
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            if (sender is HomeViewModel)
            {
                if (eventArgs != null && eventArgs.EventName == Constants.TAB_SWITCH)
                {
                    var tabbedPage = GetTabbedPage();

                    if (tabbedPage != null && tabbedPage.Children.Count > 0)
                    {
                        //trick to fix some ui bug on bottom bar navigation tab
                        var currentPage = GetContentPage(Constants.HOME); //tabbedPage.Children[0] as ContentPage;
                        tabbedPage.CurrentPage = currentPage;
                        System.Threading.Thread.Sleep(500);

                        switch (eventArgs.EventType)
                        {

                            case Constants.HOME:
                                {
                                    currentPage = GetContentPage(Constants.USER_PROFILE); //tabbedPage.Children[4] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                    System.Threading.Thread.Sleep(500);
                                    currentPage = GetContentPage(Constants.HOME); //tabbedPage.Children[0] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.DASHBOARD:
                                {
                                    currentPage = GetContentPage(Constants.DASHBOARD); //tabbedPage.Children[1] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.CONTRACTOR:
                                {
                                    currentPage = GetContentPage(Constants.CONTRACTOR); //tabbedPage.Children[2] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.HISTORY:
                                {
                                    currentPage = GetContentPage(Constants.HISTORY); //tabbedPage.Children[3] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.USER_PROFILE:
                                {
                                    currentPage = GetContentPage(Constants.USER_PROFILE); //tabbedPage.Children[4] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                        }
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    var result = await DisplayAlert("Confirmation", "Are you sure to exit this application?", "Ok", "Cancel");
            //    if (result)
            //    {
            //        if (Device.OS == TargetPlatform.Android)
            //        {
            //            //Do clean up if necessary
            //            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //        }
            //    }
            //});

            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure to exit this application?", "Ok", "Cancel", () =>
            {
                //For android
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            },
            () =>
            {
                //For IOS
            });

            return true;
            //return base.OnBackButtonPressed();
        }
    }
}