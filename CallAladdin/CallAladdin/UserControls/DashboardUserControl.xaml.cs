﻿using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallAladdin.UserControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardUserControl : Grid
	{
        //private DashboardUserControlViewModel dashboardUserControlViewModel;

		public DashboardUserControl ()
		{
			InitializeComponent ();
            //dashboardUserControlViewModel = new DashboardUserControlViewModel();
            //BindingContext = dashboardUserControlViewModel;
		}
	}
}