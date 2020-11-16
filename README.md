# XamarinForms-iOSPictureInPicture
 My Xamarin Forms implementation of the iOS Picture In Picture for video streaming

## Steps to implement it

Basically I created a UIView that contains an AVPlayer and I added that to the View of my ViewController.

* Create a PlayerView.cs file in the iOS project, and paste in the code there from this repo, ensuring that you change the name space to your name space
* Then create MainPageRenderer.cs in the iOS project, a custom PageRenderer for the MainPage and override the lifecycle functions so that you can add the PlayerView to your ViewController
* Ensure that your target is at least iOS 10 in the iOS project settings
* In the info.plist, set UIBackgroundMode to audio
* In the AppDelegate.cs file, set audio session category to AVAudioSessionCategoryPlayback or AVAudioSessionCategoryPlayAndRecord

### Notes about testing while development

You need to have an iPhone for testing PIP, and ensure that PIP is turned on in the settings.
