using CallAladdin.Renderers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace CallAladdin
{
	public partial class App : Application
	{
		public App ()
        {
            InitializeComponent();
            SetMainPage();
        }

        private void SetMainPage()
        {
            MainPage = new CustomNavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("#EDEF00"), BarTextColor = Color.Black };
        }

        public App(Dictionary<string, object> dict)
        {
            SetUpConfig(dict);
            InitializeComponent();
            SetMainPage();
        }

        private void SetUpConfig(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    GlobalConfig.Instance.SetValueByKey(item.Key, item.Value);
                }
            }

            GlobalConfig.Instance.SetValueByKey(Constants.USE_PASSWORDLESS, false); //default passwordless setting
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
