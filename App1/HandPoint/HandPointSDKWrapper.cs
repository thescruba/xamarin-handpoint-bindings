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

        public void AddEventHandler(System.EventHandler eventHandler) {
            throw new NotImplementedException();
        }
		public void RemoveEventHandler(System.EventHandler eventHandler) {
			throw new NotImplementedException();
		}

		public void InitPayment(string sharedKey) {

			//Please reference same code here https://www.handpoint.com/docs/device/Android/6.1.0/

			HandpointCredentials handpointCredentials = new HandpointCredentials(sharedKey);

			this.api = HapiFactory.GetAsyncInterface(this, Application.Context, handpointCredentials);
			Device device = new Device("some name", "address", "", ConnectionMethod.AndroidPayment);
			this.api.Connect(device);
		}

		public bool Print(string html) {
			throw new NotImplementedException();
			//this.api.PrintReceipt();
		}

		public bool Refund(decimal amount) {
			throw new NotImplementedException();
			//this.api.Refund();
		}


        public bool Sale(decimal amount) {
			throw new NotImplementedException();
			//this.api.Sale();
		}

        public void DeviceDiscoveryFinished(IList<Device> devices) {
            throw new NotImplementedException();
        }

        public void EndOfTransaction(TransactionResult result, Device device) {
            throw new NotImplementedException();
        }

        public void TransactionResultReady(TransactionResult transactionResult, Device device) {
            throw new NotImplementedException();
        }

        public void SignatureRequired(SignatureRequest request, Device device) {
            throw new NotImplementedException();
        }

        public void ConnectionStatusChanged(ConnectionStatus status, Device device) {
            throw new NotImplementedException();
        }

        public void CurrentTransactionStatus(StatusInfo info, Device device) {
            throw new NotImplementedException();
        }
    }
}