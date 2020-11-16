using System;
using AVFoundation;
using AVKit;
using Foundation;
using Sample;
using Sample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace Sample.iOS
{
    public class MainPageRenderer : PageRenderer, IAVPictureInPictureControllerDelegate
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            PlayerView.PlayerLayer.Player = Player;

            SetupPlayback();
            SetupPictureInPicturePlayback();

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            Player.Pause();
        }

        // Attempt to load and test these asset keys before playing
        static readonly NSString[] assetKeysRequiredToPlay = {
            (NSString)"playable",
            (NSString)"hasProtectedContent"
        };

        AVPictureInPictureController pictureInPictureController;

        AVPlayer player;
        AVPlayer Player
        {
            get
            {
                player = player ?? new AVPlayer();
                return player;
            }
        }

        PlayerView PlayerView
        {
            get
            {
                try
                {
                    // Not able to get Player View from Xamarin Forms yet, as it provides me with a Xamarin Forms
                    // view that cannot be converted to PlayerView format, so this is my work around.
                    return (PlayerView)View.Subviews[1];
                }
                catch
                {
                    var windowRect = UIScreen.MainScreen.Bounds;// this.View.Window.Frame;
                    var windowWidth = windowRect.Size.Width;
                    var windowHeight = windowRect.Size.Height;
                    var playerView = new PlayerView(new CoreGraphics.CGRect(0, 0, windowWidth, windowHeight));
                    var gestureRecognizer = new UITapGestureRecognizer(()=>PlayerView.Player.Play());
                    playerView.AddGestureRecognizer(gestureRecognizer);
                    View.AddSubview(playerView);
                    return playerView;
                }
            }
        }

        AVPlayerLayer PlayerLayer
        {
            get
            {
                return PlayerView.PlayerLayer;
            }
        }

        AVPlayerItem playerItem;
        AVPlayerItem PlayerItem
        {
            get
            {
                return playerItem;
            }
            set
            {
                playerItem = value;

                // If needed, configure player item here before associating it with a player
                // (example: adding outputs, setting text style rules, selecting media options)
                Player.ReplaceCurrentItemWithPlayerItem(playerItem);
            }
        }

        void SetupPlayback()
        {
            var movieURL = new NSUrl("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");

            var asset = new AVUrlAsset(movieURL, (AVUrlAssetOptions)null);

            // Create a new `AVPlayerItem` and make it our player's current item.
            //
            // Using `AVAsset` now runs the risk of blocking the current thread (the
            // main UI thread) whilst I/O happens to populate the properties. It's prudent
            // to defer our work until the properties we need have been loaded.
            //
            // These properties can be passed in at initialization to `AVPlayerItem`,
            // which are then loaded automatically by `AVPlayer`.
            PlayerItem = new AVPlayerItem(asset, assetKeysRequiredToPlay);
        }

        private void SetupPictureInPicturePlayback()
        {
            // Check to make sure Picture in Picture is supported for the current
            // setup (application configuration, hardware, etc.).
            // AVSampleBufferDisplayLayer
            if (AVPictureInPictureController.IsPictureInPictureSupported)
            {
                // Create `AVPictureInPictureController` with our `PlayerLayer`.
                // Set this as Delegate to receive callbacks for picture in picture events.
                // Add observer to be notified when PictureInPicturePossible changes value,
                // so that we can enable `PictureInPictureButton`.
                var layer = PlayerView.PlayerLayer;

                pictureInPictureController = new AVPictureInPictureController(layer);
                pictureInPictureController.Delegate = this;
            }
            else
            {
                // No Picture in Picture is enabled
            }
        }
    }
}
