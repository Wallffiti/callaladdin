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
            BindingContext = new DisclaimerViewModel(userRegistration);
		}
	}
}