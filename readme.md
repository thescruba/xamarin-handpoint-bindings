
# Xamarin Handpoint Bindings
This project provides Xamarin Bindings and an Example implementation of the Handpoint Android SDK

Currently these bindings target:
- Production SDK - 6.1.1
- Debug SDK - 6.1.0-RC.69-SNAPSHOT

The example and implementation has been tested and targeted to the Native integrated Android Payment PAX methods but can be updated to support the rest.

There is also a Native Android java example that follows from example here: https://www.handpoint.com/docs/device/Android/#elem_2AndroidTerminalIntegration

##The Xamarin SDK is not officially maintained by Handpoint, the SDK bindings were provided by the community of developers using the Handpoint Android SDK as a tool to help Xamarin developers get started with the integration. 

## Structure

- Android.HandpointSDKExample
This is a Native Android example application this can be used to target the Snap shot debug SDK needed to run on debug terminals
 
- Bindings
This Directory holds the Xamarin Bindings for the HandPointSDK there are 2 Projects for each variant (Debug/Production) being *HandPointSDK* and *HandpointPaymentSDK* 

- Xamarin.HandpointSDK.Example
This is a Xamarin Android Example application that shows an example of implementation.
 
## Notes
When deploying to a Debug PAX Terminal you need to ensure your reference the .Debug handpoint bindings projects and for production systems you reference the production SDK bindings. 

- Production Application
	* HandPointSDK
	* HandPointPaymentSDK

- Debug Application
	* HandPointSDK.Debug
	* HandPointPaymentSDK.Debug

The Android application has a placeholder to access the Nexus Snaphots  when targeting a debug terminal or to be used to retrieve an updated Handpoint SDK you will need to edit the build.gradle with your maven credentials provided by HandPoint.

