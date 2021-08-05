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
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Handpoint.Api;
using Com.Handpoint.Api.Applicationprovider;
using Com.Handpoint.Api.Shared;
using Com.Handpoint.Api.Shared.Options;
using Java.Interop;
using Xamarin.HandpointSDK.Enums;
using Xamarin.HandpointSDK.Events;
using Xamarin.HandpointSDK.HandPoint;
using Square.OkHttp3;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Essentials;
using Application = Android.App.Application;

namespace Xamarin.HandpointSDK.HandPoint {

    public class HandPointSDKWrapper : Java.Lang.Object, IIntegratedPayments, Com.Handpoint.Api.Shared.Events.IRequired, Com.Handpoint.Api.Shared.Events.IConnectionStatusChanged, Com.Handpoint.Api.Shared.Events.ICurrentTransactionStatus {

        public event EventHandler<PaymentResultEventArgs> OnPaymentResult = delegate { };
        public event EventHandler<PaymentStatusEventArgs> OnPaymentStatusUpdated = delegate { };

        private Hapi _api;
        private Context _context;
        private string _sharedSecret;
        private ConnectionStatus _status;

        public HandPointSDKWrapper() {
        }

        public bool InitPayment(Context context, string sharedSecret) {

            var result = false;

            _context = context;
            _sharedSecret = sharedSecret;

            HandpointCredentials handpointCredentials = new HandpointCredentials(_sharedSecret);

            try {

                _api = HapiFactory.GetAsyncInterface(this, _context, handpointCredentials);

                if (_api == null) {

                    return false;
                }

                // this is connect directly to to device
                Com.Handpoint.Api.Shared.Device device = new Com.Handpoint.Api.Shared.Device("LocalDevice", "LocalDevice", "", ConnectionMethod.AndroidPayment);
                result = _api.Connect(device);

            } catch (Exception ex) {

                Console.WriteLine(ex);

            }

            return result;
        }


        /// <summary>
        /// Status.
        /// </summary>
        /// <returns></returns>
        public bool Status() {

            if (_api == null) {

                return false;

            }

            return _status == ConnectionStatus.Connected;

        }

        /// <summary>
        /// Print
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public bool Print(string html) {
            return _api.PrintReceipt(html);
        }

        /// <summary>
        /// Stop the Current Transaction.
        /// </summary>
        /// <returns></returns>
        public bool StopCurrentTransaction() {
            return _api.StopCurrentTransaction();
        }

        public bool Refund(int amount) {

            if (_api != null) {

                try {

                    Java.Math.BigInteger bigInteger = new Java.Math.BigInteger(amount.ToString());
                    var currentAcctiity = ActivityProvider.CurrentActivity;
                    var result = _api.Refund(bigInteger, Currency.Gbp);
                    return result;

                } catch (Exception ex) {

                    Console.WriteLine(ex);

                    return false;

                }
            }

            return false;
        }


        /// <summary>
        /// Sale
        /// </summary>
        /// <param name="amount">Amount to be charged in fraction</param>
        /// <returns></returns>
        public bool Sale(int amount, bool tips) {

            if (_api != null) {

                try {

                    SaleOptions options = null;
                    Java.Math.BigInteger bigInteger = new Java.Math.BigInteger(amount.ToString());
                    bool result = false;

                    if (tips) {

                        options = new SaleOptions();
                        TipConfiguration config = new TipConfiguration();
                        config.EnterAmountEnabled = true;
                        config.SkipEnabled = true;
                        config.TipPercentages = new List<Java.Lang.Integer> {
                            new Java.Lang.Integer(5),
                            new Java.Lang.Integer(10),
                            new Java.Lang.Integer(15),
                            new Java.Lang.Integer(20)
                        };
                        options.TipConfiguration = config;
                        result = _api.Sale(bigInteger, Currency.Gbp, options);

                    } else {

                        result = _api.Sale(bigInteger, Currency.Gbp);

                    }

                    return result;

                } catch (Exception ex) {

                    Console.WriteLine(ex);

                    return false;

                }
            }

            return false;
        }

        /// <summary>
        /// Connection Status Changed
        /// </summary>
        /// <param name="status"></param>
        /// <param name="device"></param>
        public void ConnectionStatusChanged(ConnectionStatus status, Com.Handpoint.Api.Shared.Device device) {
            _status = status;
        }

        public void EndOfTransaction(TransactionResult result, Com.Handpoint.Api.Shared.Device device) {

            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;
            string status = result.FinStatus.ToString();
            status = txtInfo.ToTitleCase(status.ToLower());

            PaymentResults paymentResult = PaymentResults.Failed;

            try {

                paymentResult = (PaymentResults)Enum.Parse(typeof(PaymentResults), status);

            } catch { }


            if (MainThread.IsMainThread) {

                OnPaymentResult.Invoke(this, new PaymentResultEventArgs {
                    PaymentResult = paymentResult,
                    Info = result.StatusMessage,
                    AuthorisationCode = result.AuthorisationCode,
                    CustomerReceipt = result.CustomerReceipt,
                    MerchantReceipt = result.MerchantReceipt,
                    TipAmount = result.TipAmount.IntValue(),
                    TotalAmount = result.TotalAmount.IntValue(),
                    TransactionID = result.TransactionID
                });

            } else {

                MainThread.BeginInvokeOnMainThread(() => OnPaymentResult.Invoke(this, new PaymentResultEventArgs {
                    PaymentResult = paymentResult,
                    Info = result.StatusMessage,
                    AuthorisationCode = result.AuthorisationCode,
                    CustomerReceipt = result.CustomerReceipt,
                    MerchantReceipt = result.MerchantReceipt,
                    TipAmount = result.TipAmount.IntValue(),
                    TotalAmount = result.TotalAmount.IntValue(),
                    TransactionID = result.TransactionID
                }));
            }

        }

        /// <summary>
        /// Current Status info
        /// </summary>
        /// <param name="info"></param>
        /// <param name="device"></param>
        public void CurrentTransactionStatus(StatusInfo info, Com.Handpoint.Api.Shared.Device device) {

            Console.WriteLine(info.Message);

            PaymentStatusUpdates paymentStatusUpdate = PaymentStatusUpdates.NotInitialised;

            bool printerUpdate = false;

            try {

                var infoData = info.GetStatus();

                if (infoData == StatusInfo.Status.PrintingMerchantReceipt ||
                    infoData == StatusInfo.Status.PrintingCustomerReceipt ||
                    infoData == StatusInfo.Status.PrinterOutOfPaper ||
                    infoData == StatusInfo.Status.ErrorConnectingToPrinter ||
                    infoData == StatusInfo.Status.ReceiptPrintSuccess ||
                    infoData == StatusInfo.Status.AutomaticPrintingStarted
                ) {

                    printerUpdate = true;

                }

            } catch { }

            if (MainThread.IsMainThread) {

                OnPaymentStatusUpdated.Invoke(this, new PaymentStatusEventArgs {
                    PaymentStatusUpdate = paymentStatusUpdate,
                    Info = info.Message,
                    CanCancel = info.CancelAllowed,
                    PrinterMessage = printerUpdate
                });

            } else {

                MainThread.BeginInvokeOnMainThread(() => OnPaymentStatusUpdated.Invoke(this, new PaymentStatusEventArgs {
                    PaymentStatusUpdate = paymentStatusUpdate,
                    Info = info.Message,
                    CanCancel = info.CancelAllowed,
                    PrinterMessage = printerUpdate
                }));

            }

        }


        /// <summary>
        /// Transaction Result Ready.
        /// </summary>
        /// <param name="transactionResult"></param>
        /// <param name="device"></param>
        public void TransactionResultReady(TransactionResult transactionResult, Com.Handpoint.Api.Shared.Device device) {
            Console.WriteLine($"TransactionResultReady - {transactionResult}");

        }

        public void DeviceDiscoveryFinished(IList<Com.Handpoint.Api.Shared.Device> devices) {
        }

        public void SignatureRequired(SignatureRequest request, Com.Handpoint.Api.Shared.Device device) {
        }

    }

}