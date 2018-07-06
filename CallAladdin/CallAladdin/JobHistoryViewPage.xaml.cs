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
	public partial class JobHistoryViewPage : CustomPage
	{
        private JobHistoryViewViewModel jobHistoryViewModel;

        public JobHistoryViewPage (object owner)
		{
			InitializeComponent ();
            jobHistoryViewModel = new JobHistoryViewViewModel(owner);
            BindingContext = jobHistoryViewModel;
        }
	}
}