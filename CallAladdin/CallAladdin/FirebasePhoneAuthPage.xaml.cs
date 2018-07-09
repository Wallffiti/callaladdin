using CallAladdin.ViewModel;
using Newtonsoft.Json;
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
    public partial class FirebasePhoneAuthPage : CustomPage
    {
        private FirebasePhoneAuthViewModel firebasePhoneAuthViewModel;
        //public event EventHandler NavigateToHomeEventHandler;

        public FirebasePhoneAuthPage(object owner)
        {
            InitializeComponent();
            firebasePhoneAuthViewModel = new FirebasePhoneAuthViewModel(owner);
            //NavigateToHomeEventHandler += firebasePhoneAuthViewModel.NavigateToHomeHandler;
            BindingContext = firebasePhoneAuthViewModel;
            WebContent.AddLocalCallback("LoginCallback", (strResponse) =>
            {
                if (!string.IsNullOrEmpty(strResponse))
                {
                    try
                    {
                        dynamic responseData = string.IsNullOrEmpty(strResponse) ? "" : JsonConvert.DeserializeObject(strResponse);
                        if (responseData != null)
                        {
                            string uid = responseData.uid;
                            string phoneNumber = responseData.phoneNumber;

                            firebasePhoneAuthViewModel.UpdateVendorUUID(uid);
                            firebasePhoneAuthViewModel.UpdatePhone(phoneNumber);

                            dynamic stsTokenManager = responseData.stsTokenManager;

                            if (stsTokenManager != null)
                            {
                                firebasePhoneAuthViewModel.UpdateAccessToken(stsTokenManager.accessToken?.ToString());
                                firebasePhoneAuthViewModel.UpdateRefreshToken(stsTokenManager.refreshToken?.ToString());
                                //firebasePhoneAuthViewModel.UpdateExpiryTime(stsTokenManager.expirationTime);
                            }

                            //if (NavigateToHomeEventHandler != null)
                            //{
                            //    NavigateToHomeEventHandler.Invoke(this, new System.EventArgs());
                            //}

                            //Task.Run(() =>
                            //{
                            //    firebasePhoneAuthViewModel.NavigateToHome();
                            //});

                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                firebasePhoneAuthViewModel.NavigateToHome();
                            });

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Navigator.Instance.OkAlert("Error", "There is a problem with the server. Please try again later.", "OK");
            });
        }
    }
}