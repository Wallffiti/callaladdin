using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallAladdin.ViewModel;

namespace CallAladdin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SmsVerificationPage : CustomPage
	{
		public SmsVerificationPage (UserRegistration userRegistration)
		{
			InitializeComponent ();
            BindingContext = new SmsVerificationViewModel(userRegistration);
        }
	}
}