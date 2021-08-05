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

namespace Xamarin.HandpointSDK.Enums {
	public enum PaymentStatusUpdates {
		Undefined,
		Success,
		InvalidData,
		ProcessingError,
		CommandNotAllowed,
		NotInitialised,
		ConnectTimeout,
		ConnectError,
		SendingError,
		ReceivingError,
		NoDataAvailable,
		TransactionNotAllowed,
		UnsupportedCurrency,
		NoHostAvailable,
		CardReaderError,
		CardReadingFailed,
		InvalidCard,
		InputTimeout,
		UserCancelled,
		InvalidSignature,
		WaitingForCard,
		CardInserted,
		ApplicationSelection,
		ApplicationConfirmation,
		AmountValidation,
		PinInput,
		ManualCardInput,
		WaitingForCardRemoval,
		TipInput,
		SharedSecretInvalid,
		SharedSecretAuth,
		WaitingSignature,
		WaitingHostConnect,
		WaitingHostSend,
		WaitingHostReceive,
		WaitingHostDisconnect,
		PinInputCompleted,
		PosCancelled,
		RequestInvalid,
		CardCancelled,
		CardBlocked,
		RequestAuthTimeout,
		RequestPaymentTimeout,
		ResponseAuthTimeout,
		ResponsePaymentTimeout,
		IccCardSwiped,
		RemoveCard,
		ScannerIsNotSupported,
		ScannerEvent,
		BatteryTooLow,
		AccountTypeSelection,
		BtIsNotSupported,
		PaymentCodeSelection,
		PartialApproval,
		AmountDueValidation,
		InvalidUrl,
		WaitingCustomerReceipt,
		PrintingMerchantReceipt,
		PrintingCustomerReceipt,
		UpdateStarted,
		UpdateFinished,
		UpdateFailed,
		UpdateProgress,
		WaitingHostPostSend,
		WaitingHostPostReceive,
		Rebooting,
		PrinterOutOfPaper,
		ErrorConnectingToPrinter,
		CardTapped,
		ReceiptPrintSuccess,
		InvalidPinLength,
		OfflinePinAttempt,
		OfflinePinLastAttempt,
		ProcessingSignature,
		CardRemoved,
		TipEntered,
		CardLanguagePreference,
		AutomaticPrintingStarted,
		CancelOperationNotAllowed,
		UpdateSoftwareStarted,
		UpdateSoftwareFinished,
		UpdateSoftwareFailed,
		UpdateSoftwareProgress,
		InstallSoftwareStarted,
		InstallSoftwareFinished,
		InstallSoftwareFailed,
		InstallSoftwareProgress,
		UpdateConfigStarted,
		UpdateConfigFinished,
		UpdateConfigFailed,
		UpdateConfigProgress,
		InitialisationComplete
	}
}
