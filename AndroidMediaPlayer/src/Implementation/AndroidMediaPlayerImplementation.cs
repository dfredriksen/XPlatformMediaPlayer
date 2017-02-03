using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.Media;
using Android.Content;
using Android.Runtime;
using System.Threading.Tasks;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency (typeof (CYINT.XPlatformMediaPlayer.AndroidMediaPlayerImplementation))]
namespace CYINT.XPlatformMediaPlayer
{

    public abstract class AndroidMediaPlayerImplementation : XPlatformMediaPlayerImplementation
    {
        protected MediaPlayerObject _mediaPlayerObject;
        public abstract void LoadMedia();
        public abstract void OnError();
        public abstract void OnCompletion();

        public override IXPlatformMediaObject GetMediaPlayer()
        {
            return _mediaPlayerObject;
        }

        public override void SetMediaPlayer(IXPlatformMediaObject mediaPlayerObject)
        {
            _mediaPlayerObject = (MediaPlayerObject)mediaPlayerObject;
        }

        public void OnPrepared(object sender, EventArgs e)
        {

            if(GetStartPosition() > 0)
                GetMediaPlayer().SeekTo((int)GetStartPosition());

            GetMediaPlayer().SetLooping(IsMediaLooping());
            SetMediaLength(GetMediaPlayer().GetDuration());
            if(GetPlayFlag())
            {
                SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_PLAYING);
                GetMediaPlayer().Play();
            }
            else
            {
                SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_STOPPED);
            }
        }

        public void OnCompletion(object sender, EventArgs e)
        {
            SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_STOPPED);
            GetMediaPlayer().SeekTo(0);
        }

        public void OnError(object sender, MediaPlayerObject.ErrorEventArgs e)
        {
            SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_ERROR);
            throw new AndroidMediaPlayerImplementationException("Media player error: " + e.What );
        }

        public override void ResetResources()
        {           
            if(GetPlayerState() == XPlatformMediaPlayerImplementation.PLAYER_STATE_PLAYING && _mediaPlayerObject.IsPlaying)
                _mediaPlayerObject.Stop();
            
            if (GetPlayerState() != XPlatformMediaPlayerImplementation.PLAYER_STATE_NONE);
                SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_NONE);

            _mediaPlayerObject.Reset();
        }
      
    }

    public class AndroidMediaPlayerImplementationException : XPlatformMediaPlayerImplementationException    
    {
        public AndroidMediaPlayerImplementationException(string message) : base(message) { }
    }
}
