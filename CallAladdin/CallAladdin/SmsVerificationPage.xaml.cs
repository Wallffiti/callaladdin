using CallAladdin.Model;
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
	public partial class SmsVerificationPage : CustomPage
	{
		public SmsVerificationPage (UserRegistration userRegistration)
		{
			InitializeComponent ();
		}
	}
}