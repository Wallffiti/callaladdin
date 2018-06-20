using CallAladdin.Renderers;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CallAladdin
{
	public partial class MainPage : CustomPage //ContentPage
	{
        private MainViewModel mainViewModel;

		public MainPage()
		{
			InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            mainViewModel = new MainViewModel();
            BindingContext = mainViewModel;
            var assembly = typeof(MainPage);
            this.LogoImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.aladdin-logo-new-2017.png", assembly);
            this.BottomImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.blackbars.png", assembly);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            mainViewModel.IsBusy = true;
            var userProfile = await mainViewModel.UpdateUserProfile();
            if (userProfile != null)
            {
                await Task.Delay(1500); //this is a workaround, since there is an issue with xamarin on page load
                mainViewModel.NavigateToHome();   //auto navigate to home if logged in
            }
            mainViewModel.IsBusy = false;
        }
    }
}
