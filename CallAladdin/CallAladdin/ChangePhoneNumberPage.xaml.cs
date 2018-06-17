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
        private ChangePhoneNumberViewModel changePhoneNumberViewModel;

        public ChangePhoneNumberPage (object parameters)
		{
			InitializeComponent ();
            var parentViewModel = (SmsVerificationViewModel)parameters;
            changePhoneNumberViewModel = new ChangePhoneNumberViewModel(parentViewModel);
            BindingContext = changePhoneNumberViewModel;
		}

        protected override bool OnBackButtonPressed()
        {
            changePhoneNumberViewModel.CancelPhoneNumberChange();
            return true; // base.OnBackButtonPressed();
        }
    }
}