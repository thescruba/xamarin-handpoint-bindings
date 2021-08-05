/*

    Xamarin Bindings and example of use of the Handpoint Android SDK.
    Copyright (C) 2021 Jamie Simonsen (jamie@matellit.com.au)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

*/

using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Xamarin.HandpointSDK.HandPoint;
using Android.Content;
using Com.Handpoint.Api.Applicationprovider;
using Android.Content.PM;
using Xamarin.HandpointSDK.Events;
using Square.OkHttp3;
using System.Threading.Tasks;

namespace Xamarin.HandpointSDK.Example {

    [Activity(Name = "com.companyname.apphandpoint.MainActivity", Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, LaunchMode= LaunchMode.SingleTask)]
    public class MainActivity : AppCompatActivity
    {

        private HandPointSDKWrapper _handPointSDKWrapper;

        // Put your device shared secret below
        private readonly string _sharedSecret = "0102030405060708091011121314151617181920212223242526272829303132";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            var needtobecallhere = ApplicationProviderKt.Application;

            // Here we create the Wrapper and initialize it.
            _handPointSDKWrapper = new HandPointSDKWrapper();
            _handPointSDKWrapper.InitPayment(this, _sharedSecret);
            _handPointSDKWrapper.OnPaymentResult += _handPointSDKWrapper_OnPaymentResult;
            _handPointSDKWrapper.OnPaymentStatusUpdated += _handPointSDKWrapper_OnPaymentStatusUpdated;
        }

        /// <summary>
        /// Status Updated from Handpoint.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="paymStatus"></param>
        private void _handPointSDKWrapper_OnPaymentStatusUpdated(object sender, PaymentStatusEventArgs paymStatus) {
            //Here you can process the status and update UI etc.

        }

        /// <summary>
        /// Payment result raised from the EndOfTransaction Handpoint event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="paymResult"></param>
        private void _handPointSDKWrapper_OnPaymentResult(object sender, PaymentResultEventArgs paymResult) {
            
            if (paymResult.PaymentResult == Enums.PaymentResults.Authorised || paymResult.PaymentResult == Enums.PaymentResults.PartiallyApproved) {
                // This will create a task to go off and download and print the merchant copy.
                DownloadPrintReceipt(paymResult.MerchantReceipt);
            }

        }

        /// <summary>
        /// Download a Receipt.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void DownloadPrintReceipt(string url) {

            Task.Run(() => {

                try {

                    var receiptHtml = DownloadReceipt(url);
                    _handPointSDKWrapper.Print(receiptHtml);

                } catch (Exception e) {

                    Console.WriteLine(e.Message);

                }

            });

        }

        /// <summary>
        /// Download a Receipt
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string DownloadReceipt(String url) {
            try {
                ICall call = BuildGETCall(url);
                Response response = call.Execute();
                ResponseBody responseBody = response.Body();
                if (responseBody != null) {
                    return responseBody.String();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Build up a HTTP request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private ICall BuildGETCall(String url) {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .Url(url)
                    .Build();

            return client.NewCall(request);
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            _handPointSDKWrapper.Sale(100,false);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

	}
}
