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
		public MainPage()
		{
			InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            BindingContext = new MainViewModel();
            var assembly = typeof(MainPage);
            this.LogoImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.aladdin-logo-new-2017.png", assembly);
            this.BottomImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.blackbars.png", assembly);
        }
	}
}
