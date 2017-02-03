using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CYINT.XPlatformMediaPlayer
{
    public class MediaPlayerObject : MediaPlayer, IXPlatformMediaObject
    {
        public void Play()
        {
            Start();
        }

        public double GetCurrentPosition()
        {
            return CurrentPosition;
        }

        public void SetLooping(bool looping)
        {
            Looping = looping;
        }

        public bool IsLooping()
        {
            return Looping;
        }

        public int GetDuration()
        {
            return Duration;
        }
    }
}