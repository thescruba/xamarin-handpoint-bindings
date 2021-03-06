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
	public class PaymentStatusEventArgs : EventArgs {
		public PaymentStatusUpdates PaymentStatusUpdate { get; set; }
		public string Info { get; set; }
		public bool CanCancel { get; set; }
		public bool PrinterMessage { get; set; }

		public override string ToString() {
			return $"{PaymentStatusUpdate}-{Info}";
        }

    }

}
