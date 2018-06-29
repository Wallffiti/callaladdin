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
	public partial class JobRequestPage : CustomPage
	{
        JobRequestViewModel jobRequestViewModel;

		public JobRequestPage(object owner)
		{
			InitializeComponent (); 
            jobRequestViewModel = new JobRequestViewModel(owner);
            BindingContext = jobRequestViewModel;

            //POC for image compression - future reference if needed:
            //this.JobRequestCachedImage.Success += async (s, e) =>
            //{
            //    try
            //    {
            //        var test = await this.JobRequestCachedImage.GetImageAsJpgAsync();
            //        var test2 = await this.JobRequestCachedImage.GetImageAsPngAsync();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //};
            //this.JobRequestCachedImage.Finish += async (s, e) =>
            //{
            //    try
            //    {
            //        var test = await this.JobRequestCachedImage.GetImageAsJpgAsync();
            //        var test2 = await this.JobRequestCachedImage.GetImageAsPngAsync();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //};
        }

        protected override bool OnBackButtonPressed()
        {
            //jobRequestViewModel.RefreshRootPage();
            //return base.OnBackButtonPressed();

            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to exit from creating job request? All changes will be lost.", "Yes", "No", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                jobRequestViewModel.RefreshRootPage();
            }, async () =>
            {
                //For IOS
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
                jobRequestViewModel.RefreshRootPage();
            });
            return true;
        }
    }
}