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
	public partial class DisclaimerPage : CustomPage
	{
		public DisclaimerPage (UserRegistration userRegistration)
		{
			InitializeComponent ();
            var viewModel = new DisclaimerViewModel(userRegistration);
            BindingContext = viewModel;
            viewModel.ConfirmDisclaimerAcceptanceEvent += ConfirmDisclaimerAcceptance;
		}

        public void ConfirmDisclaimerAcceptance(object sender, EventArgs eventArgs)
        {
            DisplayAlert("Attention", "You have now agreed to ©2017 CallAladdin Disclaimer.", "OK");
        }
	}
}