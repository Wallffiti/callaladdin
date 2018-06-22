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

		public HomePage (UserProfile userProfile)
		{
			InitializeComponent ();
            homeViewModel = new HomeViewModel(userProfile);
            BindingContext = homeViewModel;
            homeViewModel.SubscribeMeToThis(this);
		}

        public void OnErrorHandler(object sender, ObserverErrorEventArgs eventArgs)
        {
            //if needed
        }

        public void OnUpdatedHandler(object sender, ObserverEventArgs eventArgs)
        {
            if (sender is HomeViewModel)
            {
                if (eventArgs != null && eventArgs.EventName == Constants.TAB_SWITCH)
                {
                    var tabbedPage = this as TabbedPage;

                    if (tabbedPage != null && tabbedPage.Children.Count > 0)
                    {
                        //trick to fix some ui bug on bottom bar navigation tab
                        var currentPage = tabbedPage.Children[0] as ContentPage;
                        tabbedPage.CurrentPage = currentPage;
                        System.Threading.Thread.Sleep(500);

                        switch (eventArgs.EventType)
                        {
                            case Constants.DASHBOARD:
                                {
                                    currentPage = tabbedPage.Children[1] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.USER_PROFILE:
                                {
                                    currentPage = tabbedPage.Children[4] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                }
                                break;
                            case Constants.HOME:
                                {
                                    currentPage = tabbedPage.Children[4] as ContentPage;
                                    tabbedPage.CurrentPage = currentPage;
                                    System.Threading.Thread.Sleep(500);
                                    currentPage = tabbedPage.Children[0] as ContentPage;
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