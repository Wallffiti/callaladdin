using CallAladdin.EventArgs;
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
        public UserRegistrationPage()
        {
            InitializeComponent();
            //CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            var viewModel = new UserRegistrationViewModel();
            BindingContext = viewModel;
            viewModel.OnProfilePictureChanged += ProfilePictureChangedEventHandler;
            //var assembly = typeof(UserRegistrationPage);
            //this.AvatarImage.Source = ImageSource.FromResource("CallAladdin.Assets.Images.flooring.png", assembly);
        }

        private void ProfilePictureChangedEventHandler(object sender, System.EventArgs e)
        {
            var eventArgs = (ProfilePhotoChangedEventArgs)e;
            if (eventArgs != null)
            {
                this.AvatarImage.Source = ImageSource.FromFile(eventArgs.FilePath);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure to cancel your registration? All data entered will be lost.", "Ok", "Cancel", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
            },
           () =>
           {
               //For IOS
           });

            return true;
            //return base.OnBackButtonPressed();
        }
    }
}