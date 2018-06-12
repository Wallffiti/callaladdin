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
	public partial class HomePage //: CustomPage
	{
		public HomePage ()
		{
			InitializeComponent ();
            BindingContext = new HomeViewModel();
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

            Navigator.Instance.Alert("Confirmation", "Are you sure to exit this application?", "Ok", "Cancel", () =>
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