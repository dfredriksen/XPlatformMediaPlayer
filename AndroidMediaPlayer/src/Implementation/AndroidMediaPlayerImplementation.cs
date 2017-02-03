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
        public abstract MediaPlayerObject GetSpecificPlayerObject();
        public abstract MediaPlayerObject CreateSpecificPlayerObject();

        public override IXPlatformMediaObject GetMediaPlayer()
        {
            MediaPlayerObject mediaPlayerObject = GetSpecificPlayerObject();
            if(mediaPlayerObject == null)
            {
                mediaPlayerObject = CreateSpecificPlayerObject();
                mediaPlayerObject.Prepared += OnPrepared; 
                mediaPlayerObject.Completion += OnCompletion;
                mediaPlayerObject.Error += OnError;             
            }

            return mediaPlayerObject;       
        }

        public void OnPrepared(object sender, EventArgs e)
        {
            if(GetStartPosition() > 0)
                GetMediaPlayer().SeekTo((int)GetStartPosition());

            GetMediaPlayer().SetLooping(IsMediaLooping());
            SetMediaLength(GetMediaPlayer().GetDuration());
            if(GetPlayFlag())
            {
                SetPlayerState(PLAYER_STATE_PLAYING);
                GetMediaPlayer().Play();
            }
            else
            {
                SetPlayerState(PLAYER_STATE_STOPPED);
            }
        }

        public void OnCompletion(object sender, EventArgs e)
        {
            SetPlayerState(PLAYER_STATE_STOPPED);
            GetMediaPlayer().SeekTo(0);
        }

        public void OnError(object sender, MediaPlayerObject.ErrorEventArgs e)
        {
            SetPlayerState(PLAYER_STATE_ERROR);
            throw new AndroidMediaPlayerImplementationException("Media player error: " + e.What );
        }

        public override void ResetResources()
        {           
            if(GetPlayerState() == PLAYER_STATE_PLAYING && GetSpecificPlayerObject().IsPlaying)
                GetSpecificPlayerObject().Stop();
            
            if (GetPlayerState() != PLAYER_STATE_NONE)
                SetPlayerState(PLAYER_STATE_NONE);

            GetSpecificPlayerObject().Reset();
        }
      
    }

    public class AndroidMediaPlayerImplementationException : XPlatformMediaPlayerImplementationException    
    {
        public AndroidMediaPlayerImplementationException(string message) : base(message) { }
    }
}
