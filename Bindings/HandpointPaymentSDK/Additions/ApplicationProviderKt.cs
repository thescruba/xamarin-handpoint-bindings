using System;
using Android.App;
using Android.Content;
using Android.Runtime;

namespace Com.Handpoint.Api.Applicationprovider
{
    sealed partial class ApplicationProviderKt: Java.Lang.Object
    {
        private static Application _appliation;
       
        public static unsafe global::Android.App.Application Application
        {
            [Register("getApplication", "()Landroid/app/Application;", "")]
            get
            {
                if (_appliation != null)
                {
                    return _appliation;
                }
                else

                    return initAndGetAppCtxWithReflection();
            }
        }

        private static Application initAndGetAppCtxWithReflection()
        {

            var activityThread = Java.Lang.Class.ForName("android.app.ActivityThread");
            var ctx = activityThread.GetDeclaredMethod("currentApplication").Invoke(null) as Context;

            if (ctx is Application)
            {
                _appliation =  ((Application) ctx);
                return _appliation;
            }

            return null;
        }
    }
}
