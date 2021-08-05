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
using Xamarin.HandpointSDK.Enums;
using System;

namespace Xamarin.HandpointSDK.Events {
	public class PaymentResultEventArgs : EventArgs {
		public PaymentResults PaymentResult { get; set; }
		public string Info { get; set; }
		public string AuthorisationCode { get; set; }
		public string CustomerReceipt { get; set; }
		public string MerchantReceipt { get; set; }
		public int TipAmount { get; set; }
		public int TotalAmount { get; set; }
		public string TransactionID { get; set; }

        public override string ToString() {
			return $"{PaymentResult}-{Info}";
        }

    }

}
