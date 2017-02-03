using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CYINT.XPlatformMediaPlayer
{

    public abstract class XPlatformMediaPlayerImplementation : IXPlatformMediaPlayer
    {
        public const int MEDIA_AUDIO = 1;
        public const int MEDIA_VIDEO = 2;

        public const int PLAYER_STATE_NONE = 0;
        public const int PLAYER_STATE_LOADING = 1;
        public const int PLAYER_STATE_PLAYING = 2;
        public const int PLAYER_STATE_STOPPED = 3;
        public const int PLAYER_STATE_ERROR = 4;

        protected int _mediaType;
        protected string _resource;
        protected bool _loopMedia;
        protected double _startPosition;
        protected int _state;
        protected int _progress;
        protected int _mediaLength;
        protected bool _play;

        public event StateChangeEventHandler StateChanged;

        public XPlatformMediaPlayerImplementation()
        {   
            _resource = null;
            _loopMedia = false;
            _startPosition = 0;
            _state = 0;
            _mediaLength = 0;
            _play = false;     
        }

        public abstract void LoadMedia(string resource);
        public abstract void ResetResources();
        public abstract void SetMediaPlayer(IXPlatformMediaObject mediaPlayer);
        public abstract IXPlatformMediaObject GetMediaPlayer();

        public void StopPlaying()
        {
            if(GetPlayerState() == XPlatformMediaPlayerImplementation.PLAYER_STATE_PLAYING)
            {
                GetMediaPlayer().Pause();
                SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_STOPPED);
            }
        }

        public void SetMediaLooping(bool loopMedia)
        {
            GetMediaPlayer().SetLooping(loopMedia);
        }

        public bool IsMediaLooping()
        {
            return GetMediaPlayer().IsLooping();
        }
        
        public void SetStartPosition(double startPosition)
        {
            _startPosition = startPosition;
        }

        public double GetStartPosition()
        {
            return _startPosition;
        }


        public string GetResource()
        {
            return _resource;
        }

        public void SetPlayerState(int state)
        {
            _state = state;
            if(StateChanged != null)
                StateChanged(_state);
        }

        public int GetPlayerState()
        {
            return _state;
        }

        public double GetCurrentSeekerPosition()
        {
            return GetMediaPlayer().GetCurrentPosition();
        }

        public void PlayMedia()
        {
            SetPlayFlag(true);
            if(GetPlayerState() == XPlatformMediaPlayerImplementation.PLAYER_STATE_STOPPED)
            {
                SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_PLAYING);
                GetMediaPlayer().Play();
            }
        }

        public void SetCurrentSeekerPosition(double position)
        {
            if(GetPlayerState() == XPlatformMediaPlayerImplementation.PLAYER_STATE_PLAYING || GetPlayerState() == XPlatformMediaPlayerImplementation.PLAYER_STATE_STOPPED)
                GetMediaPlayer().SeekTo((int)position);
            else
                SetStartPosition(position);
        }

        public void SetMediaLength(int length)
        {
            _mediaLength = length;
        }

        public int GetMediaLength()
        {
           return _mediaLength;
        }

        public void SetPlayFlag(bool play)
        {
            _play = play;
        }

        public bool GetPlayFlag()
        {
            return _play;
        }
    }

    public class XPlatformMediaPlayerImplementationException : Exception
    {
        public XPlatformMediaPlayerImplementationException(string message) : base(message) { }
    }
}
