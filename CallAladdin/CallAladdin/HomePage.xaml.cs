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
            if (Device.OS == TargetPlatform.Android)
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
            
            return base.OnBackButtonPressed();
        }
    }
}