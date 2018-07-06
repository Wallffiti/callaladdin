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
	public partial class ProfilePage : CustomPage
	{
        private ProfileViewModel profileViewModel;

		public ProfilePage (object owner)
		{
			InitializeComponent ();
            profileViewModel = new ProfileViewModel(owner);
            BindingContext = profileViewModel;
        }
	}
}