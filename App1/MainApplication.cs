using System;
using System.Net;
using Android.App;
using Android.Runtime;
using IO.Reactivex.Functions;
using IO.Reactivex.Plugins;

namespace App1
{ 
    [Application (Name = "com.companyname.apphandpoint.MainApplication")]
    public class MainApplication : Application, IConsumer
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public void Accept(Java.Lang.Object p0)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            RxJavaPlugins.ErrorHandler = this;

            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            ServicePointManager
                            .ServerCertificateValidationCallback +=
                            (sender, cert, chain, sslPolicyErrors) => true;
        }
    }
}
