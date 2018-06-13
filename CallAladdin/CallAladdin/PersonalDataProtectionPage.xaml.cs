using CallAladdin.Model;
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
	public partial class PersonalDataProtectionPage : CustomPage
	{
		public PersonalDataProtectionPage (UserRegistration userRegistration)
		{
			InitializeComponent ();
            var viewModel = new PersonalDataProtectionViewModel(userRegistration);
            BindingContext = viewModel;
            viewModel.AcceptAgreementEvent += ConfirmDisclaimerAcceptance;
        }

        public void ConfirmDisclaimerAcceptance(object sender, System.EventArgs eventArgs) 
        {
            DisplayAlert("Attention", "You have now agreed to ©2017 CallAladdin Disclaimer and Personal Data Protection.", "OK");
        }
    }
}