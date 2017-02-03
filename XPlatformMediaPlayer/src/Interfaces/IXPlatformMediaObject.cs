using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYINT.XPlatformMediaPlayer
{
    public interface IXPlatformMediaObject
    {
        void Stop();
        void Play();
        void Pause();
        void SeekTo(int position);
        void SetLooping(bool looping);
        bool IsLooping();
        double GetCurrentPosition();
        int GetDuration();

    }
}

