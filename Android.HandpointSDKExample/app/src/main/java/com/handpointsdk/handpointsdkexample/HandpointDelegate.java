package com.handpointsdk.handpointsdkexample;

import android.content.Context;
import android.util.Log;

import com.handpoint.api.*;
import com.handpoint.api.shared.*;

import java.math.BigInteger;
import java.util.List;

import okhttp3.*;

//Check all the events available in the Events interface.
//If you want to subscribe to more events, just add to the list of implemented interfaces.
public class HandpointDelegate implements Events.Required, Events.ConnectionStatusChanged, Events.CurrentTransactionStatus {

    private Hapi api;
    Device device;
    MainActivity activity;

    private static final String TAG = "HandpointDelegate";

    public HandpointDelegate(MainActivity myActivity) {

        activity = myActivity;
        initApi(activity);

    }

    public void initApi(Context context) {

        String sharedSecret = "0102030405060708091011121314151617181920212223242526272829303132";

        //Since we're running inside the terminal, we can create a device ourselves and connect to it
        HandpointCredentials handpointCredentials = new HandpointCredentials(sharedSecret);

        // This is the cloud version but seems to crash.
        //HandpointCredentials handpointCredentials = new HandpointCredentials(sharedSecret, cloudKey);
        this.api = HapiFactory.getAsyncInterface(this, context, handpointCredentials);

        device = new Device("Local Device", "PAXA920", "", ConnectionMethod.ANDROID_PAYMENT);
        Log.i(TAG,"Attempting to connect to device directly");

        // this is to connect to the handpoint payment app via the cloud
        /*HandpointCredentials handpointCredentials = new HandpointCredentials(sharedSecret,cloudKey);
        Device device = new Device("Cloud Device", "0821638950-PAXA920", "", ConnectionMethod.CLOUD);
        Log.i(TAG,"Attempting to connect to device via cloud");*/

        this.api.connect(device);

    }

    public void connect(){

        device = new Device("Local Device", "PAXA920", "", ConnectionMethod.ANDROID_PAYMENT);
        Log.i(TAG,"Attempting to connect to device directly");

        // this is to connect to the handpoint payment app via the cloud
        /*HandpointCredentials handpointCredentials = new HandpointCredentials(sharedSecret,cloudKey);
        Device device = new Device("Cloud Device", "0821638950-PAXA920", "", ConnectionMethod.CLOUD);
        Log.i(TAG,"Attempting to connect to device via cloud");*/

        this.api.connect(device);
    }

    @Override
    public void connectionStatusChanged(ConnectionStatus status, Device device) {
        Log.i(TAG,"connectionStatusChanged " + status);
       /* activity.printTransactionStatus("connectionStatusChanged " + status);

        if (status == ConnectionStatus.Connected) {
            activity.notifyConnectionStatus(true);
        } else if (status == ConnectionStatus.Disconnected) {
            activity.notifyConnectionStatus(false);
        }*/
    }

    @Override
    public void deviceDiscoveryFinished(List <Device> devices) {
        // This event can be safely ignored for a PAX/Telpo integration
    }

    public boolean pay() {
        Log.i(TAG,"Attempting to pay");
        return this.api.sale(new BigInteger("1000"), Currency.GBP);
        // Let´s start our first payment of 10 pounds
        // Use the currency of the country in which you will be deploying terminals
    }


    @Override
    public void currentTransactionStatus(StatusInfo statusInfo, Device device) {
        Log.i(TAG,"currentTransactionStatus " + statusInfo);
        // The StatusInfo object holds the different transaction statuses like reading card, pin entry, etc.
        // It is mandated by the card brands to display those messages to the cardholder
    }

    @Override
    public void signatureRequired(SignatureRequest signatureRequest, Device device) {
        // This event can be safely ignored for a PAX/Telpo integration
        // The complete signature capture process is already handled in the sdk, a dialog will prompt the user for a signature if required.
        // If a signature was entered, it should be printed on the receipts.
    }

    @Override
    public void endOfTransaction(TransactionResult transactionResult, Device device) {
        Log.i(TAG,"endOfTransaction " + transactionResult);
        // The TransactionResult object holds details about the transaction as well as the receipts
        // Useful information can be accessed through this object like the transaction ID, the amount, etc.
        byte[] response = downloadReceipt(transactionResult.getCustomerReceipt());
        String receipt = new String(response);
        activity.printTransactionStatus("Transaction result received");
        activity.callReceiptDialog(receipt);

    }

    private byte[] downloadReceipt(String url) {
        try {
            Call call = buildGETCall(url);
            Response response = call.execute();
            ResponseBody responseBody = response.body();
            if (responseBody != null) {
                return responseBody.bytes();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "Something went wrong".getBytes();
    }

    private Call buildGETCall(String url) {
        OkHttpClient client = new OkHttpClient();
        Request request = new Request.Builder()
                .url(url)
                .build();

        return client.newCall(request);
    }


    @Override
    public void transactionResultReady(TransactionResult transactionResult, Device device) {
        Log.i(TAG,"transactionResultReady " + transactionResult);
        // Pending TransactionResult objects will be received through this event if the EndOfTransaction
        // event was not delivered during the transaction, for example because of a network issue
        // For this sample app we are not going to implement this event
    }

    public void disconnect(){
        this.api.disconnect();
        //This disconnects the connection
    }
}