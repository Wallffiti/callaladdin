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
	public partial class JobAcceptanceViewPage : CustomPage
	{
        private JobAcceptanceViewViewModel jobAcceptanceViewViewModel;

		public JobAcceptanceViewPage (object owner)
		{
			InitializeComponent ();
            jobAcceptanceViewViewModel = new JobAcceptanceViewViewModel(owner);
            BindingContext = jobAcceptanceViewViewModel;
		}
	}
}