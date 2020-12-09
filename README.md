# XamarinForms-iOSPictureInPicture
 My Xamarin Forms implementation of the iOS Picture In Picture for video streaming

## Steps to implement it

* Follow the instructions on the Microsoft Xamarin official tutorial to setup a page displaying a video
* Then add in the info.plist, set UIBackgroundMode to audio
* In the AppDelegate.cs file, set audio session category to AVAudioSessionCategoryPlayback or AVAudioSessionCategoryPlayAndRecord

### Notes about testing while development

You need to have an iPhone for testing PIP, and ensure that PIP is turned on in the settings.
