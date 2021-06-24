using System;
using System.Collections.Generic;
using System.Text;

namespace JustTouchMobile.Services.Payments {
	public interface IPayments {

		void InitPayment(string sharedKey);
		bool Sale(decimal amount);
		bool Refund(decimal amount);
		bool Print(string html);
		void AddEventHandler(EventHandler eventHandler);
		void RemoveEventHandler(EventHandler eventHandler);

	}
}
