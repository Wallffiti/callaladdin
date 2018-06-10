using CallAladdin.Renderers;
using System;
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
            MainPage = new CustomNavigationPage(new MainPage()) { BarBackgroundColor = Color.FromHex("#EDEF00"), BarTextColor = Color.Black };
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
