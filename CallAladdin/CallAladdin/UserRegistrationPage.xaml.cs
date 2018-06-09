using CallAladdin.Renderers;
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
	public partial class UserRegistrationPage : CustomPage //ContentPage
	{
		public UserRegistrationPage ()
		{
			InitializeComponent ();
            //CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            BindingContext = new UserRegistrationViewModel(); 
            //var assembly = typeof(UserRegistrationPage);
            //this.AvatarImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.default_avatar_image.jpeg", assembly);
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
    }
}