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
	public partial class ContractorProfilePage : CustomPage
	{
        private ContractorProfileViewModel contractorProfileViewModel;

		public ContractorProfilePage (object owner)
		{
			InitializeComponent ();
            contractorProfileViewModel = new ContractorProfileViewModel(owner);
            BindingContext = contractorProfileViewModel;
		}
	}
}