﻿using CallAladdin.EventArgs;
using CallAladdin.Renderers;
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
    public partial class UserRegistrationPage : CustomPage
    {
        private UserRegistrationViewModel userRegistrationViewModel;
        private ILocationService locationService;

        public UserRegistrationPage()
        {
            InitializeComponent();
            userRegistrationViewModel = new UserRegistrationViewModel();
            locationService = new LocationService();
            BindingContext = userRegistrationViewModel;
            userRegistrationViewModel.OnProfilePictureChanged += ProfilePictureChangedEventHandler;

            PopulateLocations();
        }

        private void PopulateLocations()
        {
            Task.Run(() =>
            {
                //Long processes below
                this.ProgressIndicator.IsRunning = true; this.ProgressIndicator.IsVisible = true;
                userRegistrationViewModel.Countries = locationService.GetCountries().Result;
                userRegistrationViewModel.Cities = locationService.GetCities("all").Result;  //right now no parameter needed to filter cities
                this.ProgressIndicator.IsRunning = false; this.ProgressIndicator.IsVisible = false;
            });
        }

        private void ProfilePictureChangedEventHandler(object sender, System.EventArgs e)
        {
            var eventArgs = (ProfilePhotoChangedEventArgs)e;
            if (eventArgs != null && !string.IsNullOrEmpty(eventArgs.FilePath))
            {
                this.AvatarImage.Source = ImageSource.FromFile(eventArgs.FilePath);
                userRegistrationViewModel.UpdateUserRegistration();
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