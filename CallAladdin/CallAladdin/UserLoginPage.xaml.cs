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
	public partial class UserLoginPage : CustomPage //ContentPage
	{
        private UserLoginViewModel userLoginViewModel;

		public UserLoginPage (string emailAddress)
		{
			InitializeComponent ();
            userLoginViewModel = new UserLoginViewModel(emailAddress);
            BindingContext = userLoginViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            userLoginViewModel.ReturnMainPage();
            return true; //base.OnBackButtonPressed();
        }
    }
}