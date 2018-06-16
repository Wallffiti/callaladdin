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
	public partial class UserLoginPage : CustomPage //ContentPage
	{
		public UserLoginPage ()
		{
			InitializeComponent ();
            BindingContext = new UserLoginViewModel();

            //TODO:
            //1. Login via firebase auth api to get signupUserResponse (if db doesn't have cached data)
            //2. save signupUserResponse into local storage (if db doesn't have cached data)
            //3. Call backend server api to get user profile using data from signupUserResponse
            //4. Navigate to home page

            //userProfile = await userService.GetUserProfile("dummy");  //DEBUG

            //await Navigator.Instance.NavigateTo(PageType.USER_LOGIN); //DEBUG
            //await Navigator.Instance.NavigateTo(PageType.HOME, userProfile); //DEBUG
        }
    }
}