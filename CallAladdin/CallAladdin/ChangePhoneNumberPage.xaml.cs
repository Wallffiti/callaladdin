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
	public partial class ChangePhoneNumberPage : ContentPage //CustomPage
	{
        public ChangePhoneNumberPage (object parameters)
		{
			InitializeComponent ();
            var parentViewModel = (SmsVerificationViewModel)parameters;
            BindingContext = new ChangePhoneNumberViewModel(parentViewModel);
		}
	}
}