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
	public partial class JobViewPage : CustomPage
	{
        private JobViewViewModel jobViewViewModel;

		public JobViewPage (object owner)
		{
			InitializeComponent ();
            jobViewViewModel = new JobViewViewModel(owner);
            BindingContext = jobViewViewModel;
		}
	}
}