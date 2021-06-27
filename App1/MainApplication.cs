using System;
using Android.App;
using Android.Runtime;

namespace App1
{ 
     [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

        }
    }
}
