package com.example.myapplication;

import android.content.Context;

import com.handpoint.api.HandpointCredentials;
import com.handpoint.api.Hapi;
import com.handpoint.api.HapiFactory;
import com.handpoint.api.shared.ConnectionMethod;
import com.handpoint.api.shared.ConnectionStatus;
import com.handpoint.api.shared.Device;
import com.handpoint.api.shared.Events;
import com.handpoint.api.shared.SignatureRequest;
import com.handpoint.api.shared.StatusInfo;
import com.handpoint.api.shared.TransactionResult;

import java.util.List;

public class HandPointSDKWrapper implements Events.Required, Events.ConnectionStatusChanged, Events.CurrentTransactionStatus {

    private Hapi api;

    public HandPointSDKWrapper(Context context) {
        initApi(context);
    }

    public void initApi(Context context) {
        String sharedSecret = "5FC9200DEE75F8AB2D37B6D74E7ECB075EEA69625DCFCEC37A9A42670E16960B";
        HandpointCredentials handpointCredentials = new HandpointCredentials(sharedSecret);
        this.api = HapiFactory.getAsyncInterface(this, context, handpointCredentials);
        // The api is now initialized. Yay! we've even set default credentials.
        // The shared secret is a unique string shared between the payment terminal and your application, it is unique per merchant.
        // You should replace this default shared secret with the one sent by the Handpoint support team.
        //Since we're running inside the terminal, we can create a device ourselves and connect to it
        Device device = new Device("some name", "address", "", ConnectionMethod.ANDROID_PAYMENT);
        this.api.connect(device);
    }

    @Override
    public void connectionStatusChanged(ConnectionStatus connectionStatus, Device device) {

    }

    @Override
    public void currentTransactionStatus(StatusInfo statusInfo, Device device) {

    }

    @Override
    public void deviceDiscoveryFinished(List<Device> list) {

    }

    @Override
    public void endOfTransaction(TransactionResult transactionResult, Device device) {

    }

    @Override
    public void signatureRequired(SignatureRequest signatureRequest, Device device) {

    }

    @Override
    public void transactionResultReady(TransactionResult transactionResult, Device device) {

    }
}
