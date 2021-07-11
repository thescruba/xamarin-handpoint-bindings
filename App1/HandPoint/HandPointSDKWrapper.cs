using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Handpoint.Api;
using Com.Handpoint.Api.Shared;
using Java.Interop;
using JustTouchMobile.Services.Payments;
using System;
using System.Collections.Generic;
using Application = Android.App.Application;

namespace JustTouchMobile.Droid.HandPoint {
	class HandPointSDKWrapper : Java.Lang.Object, IPayments, Com.Handpoint.Api.Shared.Events.IRequired, Com.Handpoint.Api.Shared.Events.IConnectionStatusChanged, Com.Handpoint.Api.Shared.Events.ICurrentTransactionStatus {
		
		private Hapi api;
        private Context mContext;

        public HandPointSDKWrapper(Context context) {
            mContext = context;
        }

        public void AddEventHandler(System.EventHandler eventHandler) {
            throw new NotImplementedException();
        }
		public void RemoveEventHandler(System.EventHandler eventHandler) {
			throw new NotImplementedException();
		}

		public void InitPayment(string sharedKey) {

            //Please reference same code here https://www.handpoint.com/docs/device/Android/6.1.0/

            var cloudKey = "M9KC2NP-632MZZZ-KH6EMM5-WVMYYTM";

            HandpointCredentials handpointCredentials = new HandpointCredentials(sharedKey);

            try
            {
                this.api = HapiFactory.GetAsyncInterface(this, mContext, handpointCredentials);
                this.api.SetLogLevel(LogLevel.Debug);

                // this is connect directly to to device
                Device device = new Device("LocalDevice", "0821638950-PAXA920", "", ConnectionMethod.AndroidPayment);
                // this is to connect to the handpoint payment app via the cloud
                //Device device = new Device("Cloud Device", "0821638950-PAXA920", "", ConnectionMethod.Cloud);
                
                this.api.Connect(device);

            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
		}

		public bool Print(string html) {
			return this.api.PrintReceipt (html);
		}

		public bool Refund(decimal amount) {
			throw new NotImplementedException();
			//this.api.Refund();
		}


        public bool Sale(decimal amount) {

            if (this.api != null)
            {
                Java.Math.BigInteger bigInteger = new Java.Math.BigInteger(amount.ToString());
                return this.api.Sale(bigInteger, Currency.Usd);
            }

            return false;
		}

        public void DeviceDiscoveryFinished(IList<Device> devices) {

        }

        public void EndOfTransaction(TransactionResult result, Device device) {
            
        }

        public void TransactionResultReady(TransactionResult transactionResult, Device device) {
            
        }

        public void SignatureRequired(SignatureRequest request, Device device) {
           
        }

        public void ConnectionStatusChanged(ConnectionStatus status, Device device) {
            Console.WriteLine($"ConnectionStatusChanged - {status}");
            
            if (status == ConnectionStatus.Connected) {
                Sale(100);
            }

        }

        public void CurrentTransactionStatus(StatusInfo info, Device device) {
            Console.WriteLine($"CurrentTransactionStatus - {info}");

        }
    }
}