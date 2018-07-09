using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using Plugin.CurrentActivity;
using Xam.Plugin.WebView.Droid;

namespace CallAladdin.Droid
{
    [Activity(Label = "CallAladdin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]  //Fixed to portrait
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar; 
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            FormsWebViewRenderer.Initialize();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            var dict = new Dictionary<string, object>();
            var b = PackageManager.GetApplicationInfo(PackageName, Android.Content.PM.PackageInfoFlags.MetaData).MetaData;

            //Google map API
            var googleAndroidGeoApiKey = b.GetString("com.google.android.geo.API_KEY", "");
            dict.Add("com.google.android.geo.API_KEY", googleAndroidGeoApiKey);

            //Firebase API
            var firebaseAndroidApiKey = b.GetString("com.google.android.firebase.API_KEY", "");
            dict.Add("com.google.android.firebase.API_KEY", firebaseAndroidApiKey);
            var firebaseAuthApiUrl = b.GetString("com.google.android.firebase.restful.api.url", "");
            dict.Add("com.google.android.firebase.restful.api.url", firebaseAuthApiUrl);

            //SQL lite
            string dbname = "call_aladdin.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbname);
            dict.Add("call_aladdin.sqlite_path", fullPath);

            //Call Aladdin
            string apiUrl = b.GetString("com.call.aladdin.project.api.url", "");
            dict.Add("com.call.aladdin.project.api.url", apiUrl);
            string apiKey = b.GetString("com.call.aladdin.project.api.key", "");
            dict.Add("com.call.aladdin.project.api.key", apiKey);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            CrossCurrentActivity.Current.Init(this, bundle);

            LoadApplication(new App(dict));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

