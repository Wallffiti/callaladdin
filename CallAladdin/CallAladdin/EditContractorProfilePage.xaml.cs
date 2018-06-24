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
	public partial class EditContractorProfilePage : CustomPage
	{
        private EditContractorProfileViewModel editRequestorProfileViewModel;

        public EditContractorProfilePage (object sender)
		{
			InitializeComponent ();
            var parentViewModel = sender as UserProfileUserControlViewModel;
            editRequestorProfileViewModel = new EditContractorProfileViewModel(parentViewModel);
            BindingContext = editRequestorProfileViewModel;
            //editRequestorProfileViewModel.PopulateData(parentViewModel.UserProfile);
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to exit from editing user profile? All changes will be lost.", "Yes", "No", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                editRequestorProfileViewModel.RefreshRootPage();
            }, async () =>
            {
                //For IOS
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                editRequestorProfileViewModel.RefreshRootPage();
            });
            return true;
        }
    }
}