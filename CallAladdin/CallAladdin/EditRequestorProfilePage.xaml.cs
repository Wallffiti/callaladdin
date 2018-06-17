using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
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
	public partial class EditRequestorProfilePage : CustomPage
	{
        private EditRequestorProfileViewModel editRequestorProfileViewModel;

        public EditRequestorProfilePage (UserProfile userProfile)
		{
			InitializeComponent ();
            editRequestorProfileViewModel = new EditRequestorProfileViewModel();
            BindingContext = editRequestorProfileViewModel;
            editRequestorProfileViewModel.PopulateData(userProfile);
        }
    }
}