using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallAladdin.ViewModel;
using System.Reflection;
using System.IO;

namespace CallAladdin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmsVerificationPage : CustomPage
    {
        public SmsVerificationPage(UserRegistration userRegistration)
        {
            InitializeComponent();
            BindingContext = new SmsVerificationViewModel(userRegistration);

            //TEST (for future reference)
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(SmsVerificationPage)).Assembly;
            //Stream stream = assembly.GetManifestResourceStream("CallAladdin.Assets.HTML.passwordless_auth.html");

            //string text = "";
            //if (stream != null)
            //    using (var reader = new System.IO.StreamReader(stream))
            //    {
            //        text = reader.ReadToEnd();
            //    }
            //TEST
        }
    }
}