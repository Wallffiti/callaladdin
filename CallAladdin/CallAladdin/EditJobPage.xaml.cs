﻿using CallAladdin.ViewModel;
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
	public partial class EditJobPage : CustomPage
	{
        private EditJobViewModel editJobViewModel;

		public EditJobPage (object owner)
		{
			InitializeComponent ();
            editJobViewModel = new EditJobViewModel(owner);
            BindingContext = editJobViewModel;
		}

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            Navigator.Instance.ConfirmationAlert("Confirmation", "Are you sure you want to exit from editing job request? All changes will be lost.", "Yes", "No", async () =>
            {
                //For android
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
            }, async () =>
            {
                //For IOS
                await Navigator.Instance.ReturnPrevious(UIPageType.PAGE);
            });
            return true;
        }
    }
}