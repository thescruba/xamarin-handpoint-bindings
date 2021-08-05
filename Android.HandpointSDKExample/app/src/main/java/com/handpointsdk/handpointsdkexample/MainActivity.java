package com.handpointsdk.handpointsdkexample;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.DialogInterface;
import android.os.Bundle;
import android.view.*;
import android.webkit.*;
import android.widget.*;

public class MainActivity extends AppCompatActivity {

    private Button payNowButton;
    private Button connectButton;
    private Button disconnectButton;
    private EditText textConsole;

    private HandpointDelegate _handpointDelegate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _handpointDelegate = new HandpointDelegate(this);
        initializeComponents();
    }

    public void initializeComponents() {
        payNowButton = (Button) findViewById(R.id.payNowBtn);
        payNowButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                _handpointDelegate.pay();
            }
        });
        connectButton = (Button) findViewById(R.id.connectCardReader);
        connectButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                _handpointDelegate.connect(); // For a direct connection to the card reader
            }
        });
        disconnectButton = (Button) findViewById(R.id.disconnectCardReader);
        disconnectButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                _handpointDelegate.disconnect();
            }
        });

        textConsole = (EditText) findViewById(R.id.textConsole);
    }

    public void callReceiptDialog(final String customerReceipt) {
        this.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                String positiveButton = "OK";
                WebView webView = new WebView(MainActivity.this); // Create a webView to display the receipt
                webView.loadData(customerReceipt, "text/html", "UTF-8");
                webView.getSettings().setDefaultFontSize(20);
                webView.setVerticalScrollBarEnabled(true);
                new AlertDialog.Builder(MainActivity.this)// Defines an AlertDialog that will popup
                        .setTitle("Transaction result")
                        .setPositiveButton(positiveButton, new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int i) {
                                dialogInterface.dismiss();
                            }
                        })
                        .setView(webView)
                        .show();
            }
        });
    }

    public void notifyConnectionStatus(boolean connected) {
        this.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                String positiveButton = "OK";
                WebView webView = new WebView(MainActivity.this);

                AlertDialog.Builder alert = new AlertDialog.Builder(MainActivity.this)// Defines an AlertDialog that will popup
                        .setPositiveButton(positiveButton, new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int i) {
                                dialogInterface.dismiss();
                            }
                        })
                        .setView(webView);

                if (connected) {
                    alert.setTitle("Connected to Device");
                } else {
                    alert.setTitle("Disconnected from Device");
                }

                alert.show();
            }
        });
    }

    @Override
    public void onPause() {
        super.onPause();  // Always call the superclass method first
        _handpointDelegate.disconnect(); // disconnects from the card reader if the application is paused
    }

    @Override
    public void onResume() {
        super.onResume();  // Always call the superclass method first
    }

    @Override
    public void onBackPressed() {
        moveTaskToBack(true); // The application is moved in the background
    }

    @Override
    public void onStop() {
        super.onStop();  // Always call the superclass method first
    }

    @Override
    public void onRestart() {
        super.onRestart();  // Always call the superclass method first
        initializeComponents(); // initialize the buttons again after restarting the app
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    public void printTransactionStatus(String message) {
        this.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                textConsole.setText("Status Received -> " + message);
            }
        });
    }
}